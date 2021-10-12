using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Yolol.YololScript;
using starbase_nexus_api.Repositories.Yolol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("yolol/[controller]")]
    public class YololScriptController : DefaultControllerTemplate
    {
        private readonly IYololScriptRepository<YololScript> _yololScriptRepository;
        private readonly IYololProjectRepository<YololProject> _yololProjectRepository;
        private readonly IMapper _mapper;

        public YololScriptController(
            IYololScriptRepository<YololScript> yololScriptRepository,
            IYololProjectRepository<YololProject> yololProjectRepository,
            IMapper mapper
        )
        {
            _yololScriptRepository = yololScriptRepository;
            _yololProjectRepository = yololProjectRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewYololScript>>> GetMultiple([FromQuery] YololScriptSearchParameters parameters)
        {
            PagedList<YololScript> entities = await _yololScriptRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewYololScript>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewYololScript>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<YololScript> entities = await _yololScriptRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewYololScript>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewYololScript>> GetOne(Guid id, [FromQuery] string? fields)
        {
            YololScript? entity = await _yololScriptRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewYololScript>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewYololScript>> Create([FromBody] CreateYololScript createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            YololProject? yololProject = await _yololProjectRepository.GetOneOrDefault((Guid)createObj.ProjectId);

            if (yololProject == null)
                return BadRequest();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (yololProject.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            YololScript newEntity = _mapper.Map<YololScript>(createObj);
            newEntity = await _yololScriptRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewYololScript>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewYololScript>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchYololScript> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            YololScript? entity = await _yololScriptRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.Project.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchYololScript patchObj = _mapper.Map<PatchYololScript>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _yololScriptRepository.Update(entity);

            return Ok(_mapper.Map<ViewYololScript>(entity));
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

            YololScript? entity = await _yololScriptRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.Project.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _yololScriptRepository.Delete(entity);

            return Ok();
        }
    }
}