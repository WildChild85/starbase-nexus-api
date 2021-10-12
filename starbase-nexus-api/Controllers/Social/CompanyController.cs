using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Company;
using starbase_nexus_api.Repositories.Social;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Social
{
    [Route("social/[controller]")]
    public class CompanyController : DefaultControllerTemplate
    {
        private readonly ICompanyRepository<Company> _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(
            ICompanyRepository<Company> companyRepository,
            IMapper mapper
        )
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<ViewCompany>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            PagedList<Company> entities = await _companyRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewCompany>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ViewCompany>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            IEnumerable<Company> entities = await _companyRepository.GetMultiple(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewCompany>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ViewCompany>> GetOne(Guid id, [FromQuery] string? fields)
        {
            Company? entity = await _companyRepository.GetOneOrDefault(id);
            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<ViewCompany>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create a new one.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ViewCompany>> Create([FromBody] CreateCompany createObj)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Company newEntity = _mapper.Map<Company>(createObj);
            newEntity.CreatorId = currentUserId;
            newEntity = await _companyRepository.Create(newEntity);

            return Ok(_mapper.Map<ViewCompany>(newEntity));
        }

        /// <summary>
        /// Patch an existing one.
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<ViewCompany>> Patch(Guid id, [FromBody] JsonPatchDocument<PatchCompany> patchDocument)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            Company? entity = await _companyRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            PatchCompany patchObj = _mapper.Map<PatchCompany>(entity);

            patchDocument.ApplyTo(patchObj, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(patchObj, entity);
            await _companyRepository.Update(entity);

            return Ok(_mapper.Map<ViewCompany>(entity));
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

            Company? entity = await _companyRepository.GetOneOrDefault(id);

            if (entity == null)
                return NotFound();

            if (!CurrentUserHasRole(RoleConstants.ADMINISTRATOR))
            {
                if (entity.CreatorId != currentUserId)
                {
                    return Forbid();
                }
            }

            await _companyRepository.Delete(entity);

            return Ok();
        }
    }
}