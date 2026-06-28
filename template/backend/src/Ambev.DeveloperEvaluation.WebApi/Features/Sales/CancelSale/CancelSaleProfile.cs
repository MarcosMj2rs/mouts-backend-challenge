using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// Profile for CancelSale mappings.
    /// </summary>
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleResult, CancelSaleResponse>();
        }
    }
}
