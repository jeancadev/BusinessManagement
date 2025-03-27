using AutoMapper;
using BusinessManagement.Application.Customers.DTOs;
using BusinessManagement.Application.Inventories.DTOs;
using BusinessManagement.Application.Products.Queries;
using BusinessManagement.Application.Sales.DTOs;
using BusinessManagement.Domain.Entities;

namespace BusinessManagement.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // De Product → ProductDto y en sentido inverso
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDto>().ReverseMap();

            // De Inventory → InventoryDto
            CreateMap<Inventory, InventoryDto>();
            CreateMap<Inventory, InventoryDto>().ReverseMap();

            // De Sale → SaleDto
            CreateMap<Sale, SaleDto>();
            CreateMap<Sale, SaleDto>().ReverseMap();

            // De SaleItem → SaleItemDto y en sentido inverso
            CreateMap<SaleItem, SaleItemDto>();
            CreateMap<SaleItem, SaleItemDto>().ReverseMap();

            // De Customer -> CustomerDto
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
