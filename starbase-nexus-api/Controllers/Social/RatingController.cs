using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Rating;
using starbase_nexus_api.Repositories.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Social
{
    [Route("social/[controller]")]
    public class RatingController : DefaultControllerTemplate
    {
        private readonly IRatingRepository<Rating> _ratingRepository;
        private readonly IMapper _mapper;

        public RatingController(
            IRatingRepository<Rating> ratingRepository,
            IMapper mapper
        )
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewRating>>> GetMultiple([FromQuery] RatingSearchParameters parameters)
        {
            PagedList<Rating> entities = await _ratingRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewRating>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewRating>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Rating> entities = await _ratingRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<Rating>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewRating>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Rating? entity = await _ratingRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewRating>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewRating>> Create([FromBody] CreateRating createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            RatingSearchParameters conflictingSearchParameters = new RatingSearchParameters
            {
                UserIds = currentUserId,
                ShipIds = createObj.ShipId.ToString(),
                PageSize = 1
            };

            PagedList<Rating> conflicting = await _ratingRepository.GetMultiple(conflictingSearchParameters);

            if (conflicting.Count > 0)
            {
                return BadRequest();
            }

            Rating newEntity = _mapper.Map<Rating>(createObj);
            newEntity.UserId = currentUserId;
            newEntity = await _ratingRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewRating>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewRating>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchRating> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Rating? entity = await _ratingRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.UserId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchRating patchObj = _mapper.Map<PatchRating>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _ratingRepository.Update(entity);

            return Ok(_mapper.Map<ViewRating>(entity));
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

            Rating? entity = await _ratingRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.UserId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _ratingRepository.Delete(entity);

            return Ok();
        }
    }
}