﻿using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stargazer.Common.Extend;

namespace Stargazer.Abp.ObjectStorage.HttpApi.Controllers
{
    [Route("article/picture")]
    [ApiController]
    public class ArticlePictureController : AbpController
    {
        private readonly IArticlePictureService _articlePictureService;
        private readonly IConfiguration _configuration;

        public ArticlePictureController(
            IArticlePictureService articlePictureService,
            IConfiguration configuration)
		{
            _articlePictureService = articlePictureService;
            _configuration = configuration;
		}

        [HttpPost("")]
        [Authorize]
        public async Task<UploadResponseDto> UpdatePictureAsync()
        {
            var input = new UploadFileInfo(Request.Form.Files.First());
            var extensions = _configuration.GetSection("BlobStore:ArticlePicture:FileExtension").Value?.Split(",")?? new string[] { };
            var maxSize = _configuration.GetSection("BlobStore:ArticlePicture:MaxSize").Value?.ToInt();
            if (!extensions.Contains(input.FileExtension))
            {
                throw new FileExtensionException("请选择图片文件");
            }

            if (input.FileSize > maxSize)
            {
                throw new FileSizeException(string.Format("图片文件大小不能超过{0}M", maxSize/1024/1024));
            }
            string fileName = Guid.NewGuid().ToString("N").ToLower() + input.FileExtension;
            await _articlePictureService.SaveAsync(fileName, input.FileBytes);
            return new UploadResponseDto
            {
                Location = $"article/picture/{fileName}"
            };
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetPictureAsync(string fileName)
        {
            var picture = await _articlePictureService.GetAsync(fileName);
            string contentType = "application/octet-stream";
            return File(picture, contentType);
        }
    }
}

