using RolDbContext.Models;
using viewmodels;

namespace clientside.backend.Mappers
{
    public static class OrderMapper
    {
        public static viewmodels.Order? Map(this RolDbContext.Models.Order? order)
        {
            if (order == null) return null;
            var response = new viewmodels.Order
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryCity = order.DeliveryCity,
                DeliveryPostalCode = order.DeliveryPostalCode,
                UpdatedDate = order.UpdatedDate,
                Status = order.Status,
                Version = order.Version,
            };
            foreach (var row in order.OrderRows) {
                response.OrderRows.Add(row.Map()!);
            }
            return response;
        }
        public static RolDbContext.Models.Order? Map(this viewmodels.Order? order)
        {
            if (order == null) return null;
            var response = new RolDbContext.Models.Order
            {
                Id = order.Id ?? Guid.NewGuid(),
                CreatedDate = order.CreatedDate,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryCity = order.DeliveryCity,
                DeliveryPostalCode = order.DeliveryPostalCode,
                UpdatedDate = order.UpdatedDate,
                Status = order.Status ?? Status.SavedLocal,
                Version = order.Version < 1 ? 1 : order.Version,
                CustomerId = order.Customer!.Id!.Value
            };
            foreach (var row in order.OrderRows) {
                response.OrderRows.Add(row.Map()!);
            }
            return response;
        }
    }
}
