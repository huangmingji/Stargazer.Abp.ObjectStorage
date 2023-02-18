using AutoMapper;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using Stargazer.Abp.ObjectStorage.Domain;

namespace Stargazer.Abp.ObjectStorage.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<ObjectData, ObjectDataDto>();
        }
    }
}