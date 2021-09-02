using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ItemCategory;
using starbase_nexus_api.Repositories.InGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("ingame/[controller]")]
    public class ItemCategoryController : DefaultControllerTemplate
    {
        private readonly IItemCategoryRepository<ItemCategory> _itemCategoryRepository;
        private readonly IMapper _mapper;

        public ItemCategoryController(
            IItemCategoryRepository<ItemCategory> itemCategoryRepository,
            IMapper mapper
        )
        {
            _itemCategoryRepository = itemCategoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewItemCategory>>> GetMultiple([FromQuery] ItemCategorySearchParameters parameters)
        {
            PagedList<ItemCategory> entities = await _itemCategoryRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewItemCategory>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewItemCategory>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<ItemCategory> entities = await _itemCategoryRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ItemCategory>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewItemCategory>> GetOne(Guid id, [FromQuery] string? fields)
        {
            ItemCategory? entity = await _itemCategoryRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewItemCategory>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewItemCategory>> Create([FromBody] CreateItemCategory createObj)
        {
            ItemCategory newEntity = _mapper.Map<ItemCategory>(createObj);
            newEntity = await _itemCategoryRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewItemCategory>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewItemCategory>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchItemCategory> patchDocument)
        {
            ItemCategory? entity = await _itemCategoryRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchItemCategory patchObj = _mapper.Map<PatchItemCategory>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _itemCategoryRepository.Update(entity);

            return Ok(_mapper.Map<ViewItemCategory>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            ItemCategory? entity = await _itemCategoryRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _itemCategoryRepository.Delete(entity);

            return Ok();
        }
    }
}