using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Yolol.YololProject;
using starbase_nexus_api.Repositories.Yolol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.InGame
{
    [Route("yolol/[controller]")]
    public class YololProjectController : DefaultControllerTemplate
    {
        private readonly IYololProjectRepository<YololProject> _yololProjectRepository;
        private readonly IMapper _mapper;

        public YololProjectController(
            IYololProjectRepository<YololProject> yololProjectRepository,
            IMapper mapper
        )
        {
            _yololProjectRepository = yololProjectRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewYololProject>>> GetMultiple([FromQuery] YololProjectSearchParameters parameters)
        {
            PagedList<YololProject> entities = await _yololProjectRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewYololProject>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewYololProject>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<YololProject> entities = await _yololProjectRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewYololProject>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewYololProject>> GetOne(Guid id, [FromQuery] string? fields)
        {
            YololProject? entity = await _yololProjectRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewYololProject>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewYololProject>> Create([FromBody] CreateYololProject createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            YololProject newEntity = _mapper.Map<YololProject>(createObj);
            newEntity.CreatorId = currentUserId;
            newEntity = await _yololProjectRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewYololProject>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewYololProject>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchYololProject> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            YololProject? entity = await _yololProjectRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchYololProject patchObj = _mapper.Map<PatchYololProject>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _yololProjectRepository.Update(entity);

            return Ok(_mapper.Map<ViewYololProject>(entity));
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

            YololProject? entity = await _yololProjectRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _yololProjectRepository.Delete(entity);

            return Ok();
        }
    }
}