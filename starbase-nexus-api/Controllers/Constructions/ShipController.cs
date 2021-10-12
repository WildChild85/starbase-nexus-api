using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.Ship;
using starbase_nexus_api.Models.Constructions.ShipRoleReference;
using starbase_nexus_api.Repositories.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Social
{
    [Route("constructions/[controller]")]
    public class ShipController : DefaultControllerTemplate
    {
        private readonly IShipRepository<Ship> _shipRepository;
        private readonly IShipRoleReferenceRepository<ShipRoleReference> _shipRoleReferenceRepository;
        private readonly IMapper _mapper;

        public ShipController(
            IShipRepository<Ship> shipRepository,
            IShipRoleReferenceRepository<ShipRoleReference> shipRoleReferenceRepository,
            IMapper mapper
        )
        {
            _shipRepository = shipRepository;
            _shipRoleReferenceRepository = shipRoleReferenceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewShip>>> GetMultiple([FromQuery] ShipSearchParameters parameters)
        {
            PagedList<Ship> entities = await _shipRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewShip>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewShip>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Ship> entities = await _shipRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewShip>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewShip>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Ship? entity = await _shipRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewShip>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewShip>> Create([FromBody] CreateShip createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Ship newEntity = _mapper.Map<Ship>(createObj);

            if (createObj.IsCreator)
                newEntity.CreatorId = currentUserId;

            newEntity = await _shipRepository.Create(newEntity);

            await PatchShipRoles(newEntity, createObj.ShipRoleIds);

            return Ok(_mapper.Map<ViewShip>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewShip>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchShip> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Ship? entity = await _shipRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR) && !CurrentUserHasRole(RoleConstants.MODERATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchShip patchObj = _mapper.Map<PatchShip>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _shipRepository.Update(entity);

            await PatchShipRoles(entity, patchObj.ShipRoleIds);

            return Ok(_mapper.Map<ViewShip>(entity));
        }

        /// <summary>
        /// Assign a ship to a user.
        /// </summary>
        [HttpPatch]
        [Route("{id}/creator")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShip>> PatchCreator(Guid id, ShipCreatorAssignment shipCreatorAssignment)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Ship? entity = await _shipRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            entity.CreatorId = shipCreatorAssignment.CreatorId != null ? shipCreatorAssignment.CreatorId.ToString() : null;

            await _shipRepository.Update(entity);

            return Ok(_mapper.Map<ViewShip>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Ship? entity = await _shipRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _shipRoleReferenceRepository.DeleteRange(entity.ShipRoles);

            await _shipRepository.Delete(entity);

            return Ok();
        }

        private async Task PatchShipRoles(Ship ship, IEnumerable<Guid> shipRoleIds)
        {
            PagedList<ShipRoleReference> existing = await _shipRoleReferenceRepository.GetMultiple(
                new ShipRoleReferenceSearchParameters
                {
                    ShipIds = ship.Id.ToString(),
                    PageSize = -1
                }
            );
            var existingShipRoleIds = from reference in existing select reference.ShipRoleId;

            IEnumerable<ShipRoleReference> toDelete = existing.Where(r => !shipRoleIds.Contains(r.Id));
            IEnumerable<Guid> toCreateIds = shipRoleIds.Where(shipRoleId => !existingShipRoleIds.Contains(shipRoleId));
            List<ShipRoleReference> toCreate = new List<ShipRoleReference>();

            foreach(var toCreateId in toCreateIds)
            {
                toCreate.Add(
                    new ShipRoleReference
                    {
                        ShipId = ship.Id,
                        ShipRoleId = toCreateId
                    }
                );
            }

            await _shipRoleReferenceRepository.DeleteRange(toDelete);
            await _shipRoleReferenceRepository.CreateRange(toCreate);
        }
    }
}