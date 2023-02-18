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
using Lemon.Common.Extend;

namespace Stargazer.Abp.ObjectStorage.HttpApi.Controllers
{
    [Route("avatar")]
    [ApiController]
    public class AvatarController : AbpController
    {
        private readonly IProfilePictureService _profilePictureService;
        private readonly IObjectDataService _objectDataService;
        private readonly IConfiguration _configuration;

        public AvatarController(
            IProfilePictureService profilePictureService,
            IObjectDataService objectDataService,
            IConfiguration configuration)
        {
            _profilePictureService = profilePictureService;
            _objectDataService = objectDataService;
            _configuration = configuration;
        }

        [HttpPost("")]
        [Authorize]
        public async Task<UploadResponseDto> UpdateAvatarAsync()
        {
            var input = new UploadFileInfo(Request.Form.Files.First());
            var extensions = _configuration.GetSection("BlobStore:Avatar:FileExtension").Value.Split(",");
            var maxSize = _configuration.GetSection("BlobStore:Avatar:MaxSize").Value.ToInt();
            if (!extensions.Contains(input.FileExtension))
            {
                throw new UserFriendlyException("请选择图片文件");
            }

            if (input.FileSize > maxSize)
            {
                throw new UserFriendlyException(string.Format("图片文件大小不能超过{0}M", maxSize / 1024 / 1024));
            }

            await _objectDataService.CreateAsync(new UpdateObjectDataDto()
            {
                FileType = input.FileType,
                FileExtension = input.FileExtension,
                FileHash = input.FileHash,
                FileSize = input.FileSize,
                FilePath = input.FileHash
            });

            await _profilePictureService.SaveAsync(CurrentUser.Id.ToString(), input.FileBytes);
            return new UploadResponseDto
            {
                FileUrl = $"avatar/{CurrentUser.Id}"
            };
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAvatarAsync(Guid userId)
        {
            var picture = await _profilePictureService.GetAsync(userId.ToString());
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var t in picture)
            {
                stringBuilder.Append(t.ToString("x2"));
            }
            string fileHash = stringBuilder.ToString().Replace("-", "");
            string contentType = "application/octet-stream";
            var objectData = await _objectDataService.GetAsync(fileHash);
            switch (objectData.ObjectExtension)
            {
                case ".jpg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
            }
            return File(picture, contentType);
        }
    }
}