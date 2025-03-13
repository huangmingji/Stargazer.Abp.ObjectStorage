using System;
using System.Linq;
using System.Threading.Tasks;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using System.Text;
using Microsoft.Extensions.Configuration;
using Stargazer.Common.Extend;

namespace Stargazer.Abp.ObjectStorage.HttpApi.Controllers
{
    [Route("avatar")]
    [ApiController]
    public class AvatarController : AbpController
    {
        private readonly IProfilePictureService _profilePictureService;
        private readonly IConfiguration _configuration;

        public AvatarController(
            IProfilePictureService profilePictureService,
            IConfiguration configuration)
        {
            _profilePictureService = profilePictureService;
            _configuration = configuration;
        }

        [HttpPost("")]
        [Authorize]
        public async Task<UploadResponseDto> UpdateAvatarAsync()
        {
            var input = new UploadFileInfo(Request.Form.Files.First());
            var extensions = _configuration.GetSection("BlobStore:Avatar:FileExtension").Value?.Split(",") ?? new string[] { };
            var maxSize = _configuration.GetSection("BlobStore:Avatar:MaxSize").Value.ToInt();
            if (!extensions.Contains(input.FileExtension))
            {
                throw new FileExtensionException("请选择图片文件");
            }

            if (input.FileSize > maxSize)
            {
                throw new FileSizeException(string.Format("图片文件大小不能超过{0}M", maxSize / 1024 / 1024));
            }
            string fileName = CurrentUser.Id?.ToString() ?? Guid.NewGuid().ToString();
            await _profilePictureService.SaveAsync(fileName, input.FileBytes);
            return new UploadResponseDto
            {
                Location = $"avatar/{fileName}"
            };
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAvatarAsync(Guid userId)
        {
            var picture = await _profilePictureService.GetAsync(userId.ToString());
            string contentType = "application/octet-stream";
            return File(picture, contentType);
        }
    }
}