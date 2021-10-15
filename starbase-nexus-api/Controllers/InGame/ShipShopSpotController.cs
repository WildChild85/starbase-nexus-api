using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ShipShopSpot;
using starbase_nexus_api.Repositories.InGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("ingame/[controller]")]
    public class ShipShopSpotController : DefaultControllerTemplate
    {
        private readonly IShipShopSpotRepository<ShipShopSpot> _shipShopSpotRepository;
        private readonly IShipShopRepository<ShipShop> _shipShopRepository;
        private readonly IMapper _mapper;

        public ShipShopSpotController(
            IShipShopSpotRepository<ShipShopSpot> shipShopSpotRepository,
            IShipShopRepository<ShipShop> shipShopRepository,
            IMapper mapper
        )
        {
            _shipShopSpotRepository = shipShopSpotRepository;
            _shipShopRepository = shipShopRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewShipShopSpot>>> GetMultiple([FromQuery] ShipShopSpotSearchParameters parameters)
        {
            PagedList<ShipShopSpot> entities = await _shipShopSpotRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewShipShopSpot>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewShipShopSpot>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<ShipShopSpot> entities = await _shipShopSpotRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewShipShopSpot>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewShipShopSpot>> GetOne(Guid id, [FromQuery] string? fields)
        {
            ShipShopSpot? entity = await _shipShopSpotRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewShipShopSpot>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewShipShopSpot>> Create([FromBody] CreateShipShopSpot createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            ShipShop shipShop = await _shipShopRepository.GetOneOrDefault((Guid)createObj.ShipShopId);
            if (shipShop == null)
                return BadRequest();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR) && !CurrentUserHasRole(RoleConstants.MODERATOR))
            {
                if (shipShop.ModeratorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PagedList<ShipShopSpot> conflicts = await _shipShopSpotRepository.GetMultiple(new ShipShopSpotSearchParameters
            {
                Position = createObj.Position,
                ShipShopIds = createObj.ShipShopId.ToString(),
            });

            if (conflicts.Count > 0)
                return BadRequest();


            ShipShopSpot newEntity = _mapper.Map<ShipShopSpot>(createObj);
            newEntity = await _shipShopSpotRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewShipShopSpot>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewShipShopSpot>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchShipShopSpot> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            ShipShopSpot? entity = await _shipShopSpotRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR) && !CurrentUserHasRole(RoleConstants.MODERATOR))
            {
                if (entity.ShipShop.ModeratorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchShipShopSpot patchObj = _mapper.Map<PatchShipShopSpot>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _shipShopSpotRepository.Update(entity);

            return Ok(_mapper.Map<ViewShipShopSpot>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            ShipShopSpot? entity = await _shipShopSpotRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _shipShopSpotRepository.Delete(entity);

            return Ok();
        }
    }
}