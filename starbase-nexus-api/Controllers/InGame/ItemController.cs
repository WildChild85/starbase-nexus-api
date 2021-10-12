using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.Item;
using starbase_nexus_api.Repositories.InGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("ingame/[controller]")]
    public class ItemController : DefaultControllerTemplate
    {
        private readonly IItemRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        public ItemController(
            IItemRepository<Item> itemRepository,
            IMapper mapper
        )
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewItem>>> GetMultiple([FromQuery] ItemSearchParameters parameters)
        {
            PagedList<Item> entities = await _itemRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewItem>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewItem>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Item> entities = await _itemRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewItem>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewItem>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Item? entity = await _itemRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewItem>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewItem>> Create([FromBody] CreateItem createObj)
        {
            Item newEntity = _mapper.Map<Item>(createObj);
            newEntity = await _itemRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewItem>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewItem>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchItem> patchDocument)
        {
            Item? entity = await _itemRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchItem patchObj = _mapper.Map<PatchItem>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _itemRepository.Update(entity);

            return Ok(_mapper.Map<ViewItem>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Item? entity = await _itemRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _itemRepository.Delete(entity);

            return Ok();
        }
    }
}