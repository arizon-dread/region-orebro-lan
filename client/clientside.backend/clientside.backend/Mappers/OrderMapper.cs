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
            };
            return response;
        }
    }
}
