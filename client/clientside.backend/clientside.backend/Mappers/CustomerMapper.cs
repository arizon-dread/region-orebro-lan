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
                Version = customer.Version
            };
            return response;
        }
    }
}
