using AutoMapper;
using FinanceController.Domain.Commands;
using FinanceController.Domain.Entities;

namespace FinanceController.Domain.Api.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserCommand, User>().ReverseMap();
    }
}
