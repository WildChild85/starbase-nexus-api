using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social;
using starbase_nexus_api.Repositories.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Social
{
    [Route("social/[controller]")]
    public class PublicUserController : DefaultControllerTemplate
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PublicUserController(
            IMapper mapper,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }


        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<PagedList<PublicUser>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<User> entities = await _userRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<PublicUser>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicUser>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            return Ok(_mapper.Map<IEnumerable<PublicUser>>(await _userRepository.GetMultiple(ids, parameters)).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get one by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PublicUser>> GetOne(Guid id)
        {
            User? entity = await _userRepository.GetOneOrDefault(id.ToString());

            if (entity != null)
                return Ok(_mapper.Map<PublicUser>(entity));

            return NotFound();
        }
    }
}
