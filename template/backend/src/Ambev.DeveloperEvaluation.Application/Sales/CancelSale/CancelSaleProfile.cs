using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Profile for CancelSale mappings.
    /// </summary>
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            CreateMap<Sale, CancelSaleResult>();
        }
    }
}
