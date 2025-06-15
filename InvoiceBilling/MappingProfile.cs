using AutoMapper;
using DBLayer.Models;
using DBLayer.VModels;

namespace InvoiceBilling
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VMCustomer, Customer>();
            CreateMap<Customer, VMCustomer>();
            CreateMap<VMProduct, Product>();
            CreateMap<Product, VMProduct>();
            CreateMap<VMInvoice, Invoice>();
            CreateMap<Invoice, VMInvoice>();
            CreateMap<VMInvoiceProduct, InvoiceProduct>();
            CreateMap<InvoiceProduct, VMInvoiceProduct>();
        }
    }
}
