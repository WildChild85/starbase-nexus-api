using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Like;
using starbase_nexus_api.Repositories.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Social
{
    [Route("social/[controller]")]
    public class LikeController : DefaultControllerTemplate
    {
        private readonly ILikeRepository<Like> _likeRepository;
        private readonly IMapper _mapper;

        public LikeController(
            ILikeRepository<Like> likeRepository,
            IMapper mapper
        )
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewLike>>> GetMultiple([FromQuery] LikeSearchParameters parameters)
        {
            PagedList<Like> entities = await _likeRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewLike>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewLike>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Like> entities = await _likeRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<Like>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewLike>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Like? entity = await _likeRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewLike>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewLike>> Create([FromBody] CreateLike createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            if (createObj.YololProjectId == null /* && createObj.otherId */)
            {
                return BadRequest();
            }

            PagedList<Like> conflicting = await _likeRepository.GetMultiple(new LikeSearchParameters
            {
                UserIds = currentUserId,
                YololProjectIds = createObj.YololProjectId.ToString(),
                PageSize = 1
            });

            if (conflicting.Count > 0)
            {
                return BadRequest();
            }

            Like newEntity = _mapper.Map<Like>(createObj);
            newEntity.UserId = currentUserId;
            newEntity = await _likeRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewLike>(newEntity));
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

            Like? entity = await _likeRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.UserId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _likeRepository.Delete(entity);

            return Ok();
        }
    }
}