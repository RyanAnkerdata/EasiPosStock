using EasiPosStock.Products;
using EasiPosStock.CostCentres;
using System;
using EasiPosStock.Shared;
using Volo.Abp.AutoMapper;
using EasiPosStock.Branches;
using AutoMapper;

namespace EasiPosStock;

public class EasiPosStockApplicationAutoMapperProfile : Profile
{
    public EasiPosStockApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Branch, BranchDto>();
        CreateMap<Branch, BranchExcelDto>();

        CreateMap<CostCentre, CostCentreDto>();

        CreateMap<Product, ProductDto>();
    }
}