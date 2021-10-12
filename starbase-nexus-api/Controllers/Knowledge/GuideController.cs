using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Knowledge;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Knowledge.Guide;
using starbase_nexus_api.Repositories.Knowledge;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Knowledge
{
    [Route("knowledge/[controller]")]
    public class GuideController : DefaultControllerTemplate
    {
        private readonly IGuideRepository<Guide> _guideRepository;
        private readonly IMapper _mapper;

        public GuideController(
            IGuideRepository<Guide> guideRepository,
            IMapper mapper
        )
        {
            _guideRepository = guideRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewGuide>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<Guide> entities = await _guideRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewGuide>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewGuide>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Guide> entities = await _guideRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewGuide>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewGuide>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Guide? entity = await _guideRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewGuide>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewGuide>> Create([FromBody] CreateGuide createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Guide newEntity = _mapper.Map<Guide>(createObj);
            newEntity.CreatorId = currentUserId;
            newEntity = await _guideRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewGuide>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewGuide>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchGuide> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Guide? entity = await _guideRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchGuide patchObj = _mapper.Map<PatchGuide>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _guideRepository.Update(entity);

            return Ok(_mapper.Map<ViewGuide>(entity));
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

            Guide? entity = await _guideRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _guideRepository.Delete(entity);

            return Ok();
        }
    }
}