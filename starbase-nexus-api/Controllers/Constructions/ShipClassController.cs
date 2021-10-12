using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipClass;
using starbase_nexus_api.Repositories.Constructions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Constructions
{
    [Route("constructions/[controller]")]
    public class ShipClassController : DefaultControllerTemplate
    {
        private readonly IShipClassRepository<ShipClass> _shipClassRepository;
        private readonly IMapper _mapper;

        public ShipClassController(
            IShipClassRepository<ShipClass> shipClassRepository,
            IMapper mapper
        )
        {
            _shipClassRepository = shipClassRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewShipClass>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<ShipClass> entities = await _shipClassRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewShipClass>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewShipClass>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<ShipClass> entities = await _shipClassRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewShipClass>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewShipClass>> GetOne(Guid id, [FromQuery] string? fields)
        {
            ShipClass? entity = await _shipClassRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewShipClass>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipClass>> Create([FromBody] CreateShipClass createObj)
        {
            ShipClass newEntity = _mapper.Map<ShipClass>(createObj);
            newEntity = await _shipClassRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewShipClass>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipClass>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchShipClass> patchDocument)
        {
            ShipClass? entity = await _shipClassRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchShipClass patchObj = _mapper.Map<PatchShipClass>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _shipClassRepository.Update(entity);

            return Ok(_mapper.Map<ViewShipClass>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            ShipClass? entity = await _shipClassRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _shipClassRepository.Delete(entity);

            return Ok();
        }
    }
}