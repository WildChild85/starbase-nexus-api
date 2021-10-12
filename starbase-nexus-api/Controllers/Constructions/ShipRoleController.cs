using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipRole;
using starbase_nexus_api.Repositories.Constructions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Constructions
{
    [Route("constructions/[controller]")]
    public class ShipRoleController : DefaultControllerTemplate
    {
        private readonly IShipRoleRepository<ShipRole> _shipRoleRepository;
        private readonly IMapper _mapper;

        public ShipRoleController(
            IShipRoleRepository<ShipRole> shipRoleRepository,
            IMapper mapper
        )
        {
            _shipRoleRepository = shipRoleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewShipRole>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<ShipRole> entities = await _shipRoleRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewShipRole>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewShipRole>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<ShipRole> entities = await _shipRoleRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewShipRole>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewShipRole>> GetOne(Guid id, [FromQuery] string? fields)
        {
            ShipRole? entity = await _shipRoleRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewShipRole>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipRole>> Create([FromBody] CreateShipRole createObj)
        {
            ShipRole newEntity = _mapper.Map<ShipRole>(createObj);
            newEntity = await _shipRoleRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewShipRole>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipRole>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchShipRole> patchDocument)
        {
            ShipRole? entity = await _shipRoleRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchShipRole patchObj = _mapper.Map<PatchShipRole>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _shipRoleRepository.Update(entity);

            return Ok(_mapper.Map<ViewShipRole>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            ShipRole? entity = await _shipRoleRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _shipRoleRepository.Delete(entity);

            return Ok();
        }
    }
}