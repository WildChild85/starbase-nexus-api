using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ShipShop;
using starbase_nexus_api.Repositories.InGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("ingame/[controller]")]
    public class ShipShopController : DefaultControllerTemplate
    {
        private readonly IShipShopRepository<ShipShop> _shipShopRepository;
        private readonly IMapper _mapper;

        public ShipShopController(
            IShipShopRepository<ShipShop> shipShopRepository,
            IMapper mapper
        )
        {
            _shipShopRepository = shipShopRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewShipShop>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<ShipShop> entities = await _shipShopRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewShipShop>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewShipShop>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<ShipShop> entities = await _shipShopRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewShipShop>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewShipShop>> GetOne(Guid id, [FromQuery] string? fields)
        {
            ShipShop? entity = await _shipShopRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewShipShop>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipShop>> Create([FromBody] CreateShipShop createObj)
        {
            ShipShop newEntity = _mapper.Map<ShipShop>(createObj);
            newEntity = await _shipShopRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewShipShop>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipShop>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchShipShop> patchDocument)
        {
            ShipShop? entity = await _shipShopRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchShipShop patchObj = _mapper.Map<PatchShipShop>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _shipShopRepository.Update(entity);

            return Ok(_mapper.Map<ViewShipShop>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            ShipShop? entity = await _shipShopRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _shipShopRepository.Delete(entity);

            return Ok();
        }
    }
}