using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipMaterialCost;
using starbase_nexus_api.Repositories.Constructions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Social
{
    [Route("constructions/[controller]")]
    public class ShipMaterialCostController : DefaultControllerTemplate
    {
        private readonly IShipMaterialCostRepository<ShipMaterialCost> _shipMaterialCostRepository;
        private readonly IShipRepository<Ship> _shipRepository;
        private readonly IMapper _mapper;

        public ShipMaterialCostController(
            IShipRepository<Ship> shipRepository,
            IMapper mapper
        )
        {
            _shipRepository = shipRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewShipMaterialCost>>> GetMultiple([FromQuery] ShipMaterialCostSearchParameters parameters)
        {
            PagedList<ShipMaterialCost> entities = await _shipMaterialCostRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewShipMaterialCost>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewShipMaterialCost>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<ShipMaterialCost> entities = await _shipMaterialCostRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewShipMaterialCost>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewShipMaterialCost>> GetOne(Guid id, [FromQuery] string? fields)
        {
            ShipMaterialCost? entity = await _shipMaterialCostRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewShipMaterialCost>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewShipMaterialCost>> Create([FromBody] CreateShipMaterialCost createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Ship? ship = await _shipRepository.GetOneOrDefault((Guid)createObj.ShipId);

            if (ship == null)
                return BadRequest();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR) && !CurrentUserHasRole(RoleConstants.MODERATOR))
            {
                if (ship.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            ShipMaterialCost newEntity = _mapper.Map<ShipMaterialCost>(createObj);

            newEntity = await _shipMaterialCostRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewShipMaterialCost>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewShipMaterialCost>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchShipMaterialCost> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            ShipMaterialCost? entity = await _shipMaterialCostRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR) && !CurrentUserHasRole(RoleConstants.MODERATOR))
            {
                if (entity.Ship.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchShipMaterialCost patchObj = _mapper.Map<PatchShipMaterialCost>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _shipMaterialCostRepository.Update(entity);

            return Ok(_mapper.Map<ViewShipMaterialCost>(entity));
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

            ShipMaterialCost? entity = await _shipMaterialCostRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.Ship.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _shipMaterialCostRepository.Delete(entity);

            return Ok();
        }
    }
}