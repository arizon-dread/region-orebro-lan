using RolDbContext.Models;
using viewmodels;

namespace clientside.backend.Mappers
{
    public static class CustomerMapper
    {
        public static viewmodels.Customer? Map(this RolDbContext.Models.Customer? customer)
        {
            if (customer == null) return null;
            var response = new viewmodels.Customer
            {
                Id = customer.Id,
                CreatedDate = customer.CreatedDate,
                Name = customer.Name,
                Status = customer.Status,
                UpdatedDate = customer.UpdatedDate,
                Version = customer.Version,
                DeliveryAddress = customer.DeliveryAddress,
                DeliveryCity = customer.DeliveryCity,
                DeliveryPostalCode = customer.DeliveryPostalCode,
                Active = customer.Active
            };
            return response;
        }
        public static RolDbContext.Models.Customer? Map(this viewmodels.Customer? customer)
        {
            if (customer == null) return null;
            var response = new RolDbContext.Models.Customer
            {
                Id = customer.Id ?? Guid.NewGuid(),
                CreatedDate = customer.CreatedDate,
                UpdatedDate = customer.UpdatedDate,
                Status = customer.Status ?? Status.SavedLocal,
                Version = customer.Version < 1 ? 1 : customer.Version,
                Name = customer.Name,
                DeliveryAddress = customer.DeliveryAddress,
                DeliveryCity = customer.DeliveryCity,
                DeliveryPostalCode = customer.DeliveryPostalCode,
                Active = customer.Active
            };
            return response;
        }
        public static void MapTo(this viewmodels.Customer vmCustomer, RolDbContext.Models.Customer dbCustomer)
        {
            dbCustomer.Version = vmCustomer.Version;
            dbCustomer.Status = vmCustomer.Status ?? Status.SavedLocal;
            dbCustomer.Name = vmCustomer.Name;
            dbCustomer.UpdatedDate = vmCustomer.UpdatedDate;
            dbCustomer.CreatedDate = vmCustomer.CreatedDate;
            dbCustomer.DeliveryAddress = vmCustomer.DeliveryAddress;
            dbCustomer.DeliveryCity = vmCustomer.DeliveryCity;
            dbCustomer.DeliveryPostalCode = vmCustomer.DeliveryPostalCode;
            dbCustomer.Active = vmCustomer.Active;
        }

        public static void MapTo(this RolDbContext.Models.Customer dbCustomer, viewmodels.Customer vmCustomer)
        {
            vmCustomer.Version = dbCustomer.Version;
            vmCustomer.Status = dbCustomer.Status;
            vmCustomer.Name = dbCustomer.Name;
            vmCustomer.UpdatedDate = dbCustomer.UpdatedDate;
            vmCustomer.CreatedDate = dbCustomer.CreatedDate;
            vmCustomer.DeliveryAddress = dbCustomer.DeliveryAddress;
            vmCustomer.DeliveryCity = dbCustomer.DeliveryCity;
            vmCustomer.DeliveryPostalCode = dbCustomer.DeliveryPostalCode;
            vmCustomer.Active = dbCustomer.Active;
        }
    }
}
