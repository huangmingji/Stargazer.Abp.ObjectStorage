using System.IO;
using Lemon.Common.Extend;
using Stargazer.Abp.ObjectStorage.Application.Contracts;
using Stargazer.Abp.ObjectStorage.Application.ObjectStorageContainers;
using Stargazer.Abp.ObjectStorage.Domain.Shared.MultiTenancy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Aliyun;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.BlobStoring.Minio;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.ObjectStorage.Application
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule),
        typeof(AbpBlobStoringMinioModule),
        typeof(AbpBlobStoringAliyunModule)
    )]
    public class StargazerAbpObjectStorageApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAutoMapperObjectMapper<StargazerAbpObjectStorageApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<ApplicationAutoMapperProfile>(validate: true);
            });
            ConfigureBlobStoring(context, configuration);
        }

        private void ConfigureBlobStoring(ServiceConfigurationContext context, IConfiguration configuration)
        {
            Configure<AbpBlobStoringOptions>(options =>
            {
                options.Containers.Configure<ProfilePictureContainer>(container =>
                {
                    container.IsMultiTenant = false;
                    SetBlobStore(configuration, ref container);
                }).Configure<ArticlePictureContainer>(container =>
                {
                    container.IsMultiTenant = MultiTenancyConsts.IsEnabled;
                    SetBlobStore(configuration, ref container);
                }).Configure<OtherPictureContainer>(container =>
                {
                    container.IsMultiTenant = MultiTenancyConsts.IsEnabled;
                    SetBlobStore(configuration, ref container);
                }).Configure<OtherObjectContainer>(container =>
                {
                    container.IsMultiTenant = MultiTenancyConsts.IsEnabled;
                    SetBlobStore(configuration, ref container);
                }).Configure<MediaContainer>(container =>
                {
                    container.IsMultiTenant = MultiTenancyConsts.IsEnabled;
                    SetBlobStore(configuration, ref container);
                }).Configure<DownloadContainer>(container =>
                {
                    container.IsMultiTenant = MultiTenancyConsts.IsEnabled;
                    SetBlobStore(configuration, ref container);
                }).Configure<OfficeContainer>(container =>
                {
                    container.IsMultiTenant = MultiTenancyConsts.IsEnabled;
                    SetBlobStore(configuration, ref container);
                });
            });
        }

        private void SetBlobStore(IConfiguration configuration, ref BlobContainerConfiguration containerConfiguration)
        {
            var blobStore = configuration["BlobStore:Name"];
            switch (blobStore)
            {
                case "FileSystem":
                    containerConfiguration.UseFileSystem(fileSystem =>
                    {
                        fileSystem.BasePath = Path.Combine(Directory.GetCurrentDirectory(),
                            configuration["BlobStore:FileSystem:BasePath"]);
                    });
                    break;
                case "Minio":
                    containerConfiguration.UseMinio(minio =>
                    {
                        minio.EndPoint = configuration["BlobStore:Minio:EndPoint"];
                        minio.AccessKey = configuration["BlobStore:Minio:AccessKey"];
                        minio.SecretKey = configuration["BlobStore:Minio:SecretKey"];
                        minio.BucketName = configuration["BlobStore:Minio:BucketName"];
                    });
                    break;
                case "Aliyun":
                    containerConfiguration.UseAliyun(aliyun =>
                    {
                        aliyun.AccessKeyId = configuration["BlobStore:Aliyun:AccessKeyId"];
                        aliyun.AccessKeySecret = configuration["BlobStore:Aliyun:AccessKeySecret"];
                        aliyun.Endpoint = configuration["BlobStore:Aliyun:Endpoint"];
                        aliyun.RegionId = configuration["BlobStore:Aliyun:RegionId"];
                        aliyun.RoleArn = configuration["BlobStore:Aliyun:RoleArn"];
                        aliyun.RoleSessionName = configuration["BlobStore:Aliyun:RoleSessionName"];
                        aliyun.Policy = configuration["BlobStore:Aliyun:Policy"];
                        aliyun.DurationSeconds = configuration["BlobStore:Aliyun:DurationSeconds"].ToInt();
                        aliyun.ContainerName = configuration["BlobStore:Aliyun:ContainerName"];
                        aliyun.CreateContainerIfNotExists = true;
                    });
                    break;
            }
        }
    }
}