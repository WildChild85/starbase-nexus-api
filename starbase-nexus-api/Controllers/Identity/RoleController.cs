using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Identity.Role;
using starbase_nexus_api.Repositories.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Identity
{
    [Route("identity/[controller]")]
    public class RoleController : DefaultControllerTemplate
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;


        public RoleController(
            IRoleRepository roleRepository,
            IMapper mapper
        )
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewRole>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<Role> roles = await _roleRepository.GetMultiple(parameters);
            SetPaginationHeaders(roles);

            return Ok(_mapper.Map<IEnumerable<ViewRole>>(roles).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewRole>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            return Ok(_mapper.Map<IEnumerable<ViewRole>>(await _roleRepository.GetMultiple(ids, parameters)).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewRole>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Role? entity = await _roleRepository.GetOneOrDefault(id.ToString());
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewRole>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult<ViewRole>> Create([FromBody] CreateRole createObj)
        {
            Role newEntity = _mapper.Map<Role>(createObj);
            IdentityResult result = await _roleRepository.Create(newEntity);

            if (result.Succeeded)
                return Ok(_mapper.Map<ViewRole>(newEntity));
            else
                return BadRequest(result.Errors);
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult<ViewRole>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchRole> patchDocument)
        {
            Role? entity = await _roleRepository.GetOneOrDefault(id.ToString());

            if (entity == null)
                return NotFound();

            PatchRole patchObj = _mapper.Map<PatchRole>(entity);
            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _roleRepository.Update(entity);

            return Ok(_mapper.Map<ViewRole>(entity));
        }

        /// <summary>
        /// Delete an existing one.
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> Delete(Guid id)
        {
            Role? entity = await _roleRepository.GetOneOrDefault(id.ToString());

            if (entity == null)
                return NotFound();

            IdentityResult result = await _roleRepository.Delete(entity);
            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }
    }
}
