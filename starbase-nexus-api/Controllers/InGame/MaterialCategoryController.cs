using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.MaterialCategory;
using starbase_nexus_api.Repositories.InGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("ingame/[controller]")]
    public class MaterialCategoryController : DefaultControllerTemplate
    {
        private readonly IMaterialCategoryRepository<MaterialCategory> _materialCategoryRepository;
        private readonly IMapper _mapper;

        public MaterialCategoryController(
            IMaterialCategoryRepository<MaterialCategory> materialCategoryRepository,
            IMapper mapper
        )
        {
            _materialCategoryRepository = materialCategoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewMaterialCategory>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<MaterialCategory> entities = await _materialCategoryRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewMaterialCategory>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewMaterialCategory>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<MaterialCategory> entities = await _materialCategoryRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewMaterialCategory>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewMaterialCategory>> GetOne(Guid id, [FromQuery] string? fields)
        {
            MaterialCategory? entity = await _materialCategoryRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewMaterialCategory>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewMaterialCategory>> Create([FromBody] CreateMaterialCategory createObj)
        {
            MaterialCategory newEntity = _mapper.Map<MaterialCategory>(createObj);
            newEntity = await _materialCategoryRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewMaterialCategory>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewMaterialCategory>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchMaterialCategory> patchDocument)
        {
            MaterialCategory? entity = await _materialCategoryRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchMaterialCategory patchObj = _mapper.Map<PatchMaterialCategory>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _materialCategoryRepository.Update(entity);

            return Ok(_mapper.Map<ViewMaterialCategory>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            MaterialCategory? entity = await _materialCategoryRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _materialCategoryRepository.Delete(entity);

            return Ok();
        }
    }
}