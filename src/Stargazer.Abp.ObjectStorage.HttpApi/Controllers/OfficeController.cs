using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using Stargazer.Common.Extend;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Users;

namespace Stargazer.Abp.ObjectStorage.HttpApi.Controllers;

[Route("office")]
[ApiController]
public class OfficeController : AbpController
{
    private readonly IOfficeService _officeService;

    private readonly IConfiguration _configuration;

    public OfficeController(IOfficeService officeService, IConfiguration configuration)
    {
        this._officeService = officeService;
        this._configuration = configuration;
    }

    [HttpGet("{fileName}")]
    [Authorize]
    public async Task<IActionResult> GetAsync(string fileName)
    {
        string filePath = $"{CurrentUser.GetId()}/{fileName}";
        var picture = await _officeService.GetAsync(filePath);
        string contentType = "application/octet-stream";
        return File(picture, contentType);
    }

    [HttpPost("")]
    [Authorize]
    public async Task<UploadResponseDto> PostAsync()
    {
        var input = new UploadFileInfo(Request.Form.Files.First());
        var extensions = _configuration.GetSection("BlobStore:Office:FileExtension").Value?.Split(",") ?? [];
        var maxSize = _configuration.GetSection("BlobStore:Office:MaxSize").Value?.ToInt();
        if (!extensions.Contains(input.FileExtension))
        {
            throw new FileExtensionException("请选择office文件");
        }

        if (input.FileSize > maxSize)
        {
            throw new FileSizeException(string.Format("office文件大小不能超过{0}M", maxSize / 1024 / 1024));
        }
        string fileName = $"{input.FileName}.{input.FileExtension}";
        await _officeService.SaveAsync($"{CurrentUser.GetId()}/{fileName}", input.FileBytes);
        return new UploadResponseDto
        {
            Location = fileName
        };
    }
}