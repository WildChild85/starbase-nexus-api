using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.Material;
using starbase_nexus_api.Repositories.InGame;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("ingame/[controller]")]
    public class MaterialController : DefaultControllerTemplate
    {
        private readonly IMaterialRepository<Material> _materialRepository;
        private readonly IMapper _mapper;

        public MaterialController(
            IMaterialRepository<Material> materialRepository,
            IMapper mapper
        )
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<PagedList<ViewMaterial>>> GetMultiple([FromQuery] MaterialSearchParameters parameters)
        {
            PagedList<Material> entities = await _materialRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewMaterial>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        public async Task<ActionResult<IEnumerable<ViewMaterial>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Material> entities = await _materialRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<Material>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ViewMaterial>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Material? entity = await _materialRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewMaterial>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewMaterial>> Create([FromBody] CreateMaterial createObj)
        {
            Material newEntity = _mapper.Map<Material>(createObj);
            newEntity = await _materialRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewMaterial>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMIN_OR_MODERATOR)]
        public async Task<ActionResult<ViewMaterial>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchMaterial> patchDocument)
        {
            Material? entity = await _materialRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            PatchMaterial patchObj = _mapper.Map<PatchMaterial>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _materialRepository.Update(entity);

            return Ok(_mapper.Map<ViewMaterial>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Material? entity = await _materialRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            await _materialRepository.Delete(entity);

            return Ok();
        }
    }
}