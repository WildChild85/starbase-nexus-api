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
using starbase_nexus_api.Services.Cdn;
using starbase_nexus_api.Exceptions;
using starbase_nexus_api.Models.Cdn;
using starbase_nexus_api.Constants;

namespace starbase_nexus_api.Controllers.Cdn
{
    [Route("cdn/[controller]")]
    public class CdnController : DefaultControllerTemplate
    {
        private readonly ICdnService _cdnService;

        public CdnController(
            ICdnService cdnService
        )
        {
            _cdnService = cdnService;
        }


        /// <summary>
        /// Get the content of a folder.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<FolderContentResponse> GetMultiple([FromQuery] string path = "")
        {
            string? currentUserId = GetCurrentUserId();
            return Ok(_cdnService.ListFolderContents(path, currentUserId));
        }

        [HttpPost]
        [Route("create-folder")]
        public ActionResult CreateFolder([FromBody] CreateFolder createFolder)
        {
            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            try
            {
                _cdnService.CreateFolder(createFolder.FolderName, currentUserId, createFolder.Path);
            }
            catch (FileExistsException)
            {
                return Conflict();
            }
            catch (AccessDeniedException)
            {
                return Forbid();
            }



            return Ok();
        }


        /// <summary>
        /// Handle a file upload.
        /// </summary>
        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        [RequestFormLimits(MultipartBodyLengthLimit = 10485760)]
        public async Task<ActionResult<FileResponse>> HandleFileUpload([FromQuery] bool skipExisting = false, [FromQuery] string path = "")
        {
            if (!ModelState.IsValid)
                return Unauthorized();

            string? currentUserId = GetCurrentUserId();
            if (currentUserId == null)
                return Unauthorized();

            if (Request.Form.Files.Count != 1)
                return BadRequest();

            Microsoft.AspNetCore.Http.IFormFile uploadedFile = Request.Form.Files[0];

            try
            {
                FileResponse fileResponse = await _cdnService.HandleFileUpload(uploadedFile, currentUserId, path, skipExisting);
                return Ok(fileResponse);
            }
            catch (FileExistsException)
            {
                return Conflict();
            }
            catch (FileTypeNotAllowedException)
            {
                return BadRequest(new ErrorResponse(new List<string>() { ErrorCodes.FILETYPE_NOT_ALLOWED }));
            }
            catch (AccessDeniedException)
            {
                return Forbid();
            }

        }
    }
}
