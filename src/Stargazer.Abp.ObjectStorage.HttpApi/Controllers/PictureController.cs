using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Lemon.Common.Extend;

namespace Stargazer.Abp.ObjectStorage.HttpApi.Controllers
{
    [Route("picture")]
    [ApiController]
    public class PictureController : AbpController
    {
        private readonly IOtherPictureService _otherPictureService;
        private readonly IConfiguration _configuration;

        public PictureController(
            IOtherPictureService otherPictureService,
            IConfiguration configuration)
		{
            _otherPictureService = otherPictureService;
            _configuration = configuration;
		}

        [HttpPost("")]
        [Authorize]
        public async Task<UploadResponseDto> UpdatePictureAsync()
        {
            var input = new UploadFileInfo(Request.Form.Files.First());
            var extensions = _configuration.GetSection("BlobStore:Picture:FileExtension").Value?.Split(",")?? new string[] { };
            var maxSize = _configuration.GetSection("BlobStore:Picture:MaxSize").Value.ToInt();
            if (!extensions.Contains(input.FileExtension))
            {
                throw new UserFriendlyException("请选择图片文件");
            }

            if (input.FileSize > maxSize)
            {
                throw new UserFriendlyException(string.Format("图片文件大小不能超过{0}M", maxSize / 1024 / 1024));
            }

            string fileName = Guid.NewGuid().ToString();
            await _otherPictureService.SaveAsync(fileName, input.FileBytes);
            return new UploadResponseDto
            {
                Location = $"picture/{fileName}"
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPictureAsync(Guid id)
        {
            var picture = await _otherPictureService.GetAsync(id.ToString());
            string contentType = "application/octet-stream";
            return File(picture, contentType);
        }
    }
}

