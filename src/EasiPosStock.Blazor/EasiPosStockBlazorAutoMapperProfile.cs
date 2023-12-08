using Volo.Abp.AutoMapper;
using EasiPosStock.Branches;
using AutoMapper;

namespace EasiPosStock.Blazor;

public class EasiPosStockBlazorAutoMapperProfile : Profile
{
    public EasiPosStockBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.

        CreateMap<BranchDto, BranchUpdateDto>();
    }
}