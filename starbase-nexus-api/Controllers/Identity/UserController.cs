using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Identity.User;
using starbase_nexus_api.Repositories.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Identity
{
    [Route("identity/[controller]")]
    public class UserController : DefaultControllerTemplate
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserController(
            IMapper mapper,
            IUserRepository userRepository,
            IRoleRepository roleRepository
        )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }


        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult<PagedList<ViewUser>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<User> entities = await _userRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            IEnumerable<ViewUser> viewUsers = _mapper.Map<IEnumerable<ViewUser>>(entities);

            foreach (ViewUser viewUser in viewUsers)
            {
                User? user = (from entity in entities where entity.Id == viewUser.Id select entity).FirstOrDefault();
                if (user != null)
                {
                    viewUser.Roles = (List<string>)(await _userRepository.GetUserRoles(user));
                }
            }

            return Ok(viewUsers.ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult<IEnumerable<ViewUser>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<string> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<User> entities = await _userRepository.GetMultiple(ids, parameters);

            IEnumerable<ViewUser> viewUsers = _mapper.Map<IEnumerable<ViewUser>>(entities);

            foreach (ViewUser viewUser in viewUsers)
            {
                User? user = (from entity in entities where entity.Id == viewUser.Id select entity).FirstOrDefault();
                if (user != null)
                {
                    viewUser.Roles = (List<string>)(await _userRepository.GetUserRoles(user));
                }
            }

            return Ok(viewUsers.ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get one by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ViewUser>> GetOne(Guid id)
        {
            User? entity = await _userRepository.GetOneOrDefault(id.ToString());

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (GetCurrentUserId() != entity.Id)
                {
                    return Forbid();
                }
            }

            ViewUser viewUser = _mapper.Map<ViewUser>(entity);
            viewUser.Roles = (List<string>)(await _userRepository.GetUserRoles(entity));
            return Ok(viewUser);
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult<ViewUser>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchUser> patchDocument)
        {
            User? entity = await _userRepository.GetOneOrDefault(id.ToString());

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (GetCurrentUserId() != entity.Id)
                {
                    return Forbid();
                }
            }

            PatchUser patchObj = _mapper.Map<PatchUser>(entity);
            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            _mapper.Map(patchObj, entity);

            if (entity.AvatarUri != null && entity.AvatarUri.Trim() == "")
            {
                entity.AvatarUri = null;
            }

            if (entity.Email != entity.UserName)
            {
                entity.UserName = entity.Email;
            }

            await _userRepository.Update(entity);

            return Ok(_mapper.Map<ViewUser>(entity));
        }

        /// <summary>
        /// Lock or unlock user.
        /// </summary>
        [HttpPatch]
        [Route("{id}/lock-unlock")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> LockOrUnlockUser(Guid id, [FromBody] LockUnlockRequest lockUnlockRequest)
        {
            User? user = await _userRepository.GetOneOrDefault(id.ToString());

            if (user != null)
            {
                IdentityResult result = await _userRepository.SetUserLockout(user, lockUnlockRequest.LockUser);
                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors);
            }

            return NotFound();
        }

        /// <summary>
        /// Assign user to role.
        /// </summary>
        [HttpPatch]
        [Route("{id}/assign-user-to-role")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> AssignUserToRole(Guid id, [FromBody] UserRoleChangeRequest userRoleChangeRequest)
        {
            User? user = await _userRepository.GetOneOrDefault(id.ToString());
            Role? role = await _roleRepository.GetOneOrDefault(userRoleChangeRequest.RoleId.ToString());


            if (user == null || role == null)
                return NotFound();

            IdentityResult result = await _userRepository.AssignUserToRole(user, role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Remove user from role.
        /// </summary>
        [HttpPatch]
        [Route("{userId}/remove-user-from-role")]
        [Authorize(Roles = RoleConstants.ADMINISTRATOR)]
        public async Task<ActionResult> RemoveUserFromRole(Guid userId, [FromBody] UserRoleChangeRequest userRoleChangeRequest)
        {
            User? user = await _userRepository.GetOneOrDefault(userId.ToString());
            Role? role = await _roleRepository.GetOneOrDefault(userRoleChangeRequest.RoleId.ToString());

            if (user == null || role == null)
                return NotFound();

            IdentityResult result = await _userRepository.RemoveUserFromRole(user, role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Create admin user and role
        /// </summary>
        [HttpPatch]
        [Route("initialize")]
        [AllowAnonymous]
        public async Task<ActionResult> InitializeAdminUserAndRoles()
        {
            foreach (string roleName in RoleConstants.ROLES)
            {
                Role? role = await _roleRepository.GetOneOrDefaultByMame(roleName);
                if (role == null)
                {
                    role = new Role { Name = RoleConstants.ADMINISTRATOR };
                    await _roleRepository.Create(role);
                }
            }

            User? user = (await _userRepository.GetMultiple(new SearchParameters { OrderBy = "createdAt asc", PageSize = 1 })).FirstOrDefault();
            if (user == null)
                return BadRequest();


            Role? adminRole = await _roleRepository.GetOneOrDefaultByMame(RoleConstants.ADMINISTRATOR);
            if (adminRole == null)
                return BadRequest();

            IdentityResult roleResult = await _userRepository.AssignUserToRole(user, adminRole);
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }
            return Ok();
        }
    }
}
