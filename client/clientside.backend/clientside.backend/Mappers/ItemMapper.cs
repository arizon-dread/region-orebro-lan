namespace clientside.backend.Mappers
{
    public static class ItemMapper
    {
        public static viewmodels.Item? Map(this RolDbContext.Models.Item? item)
        {
            if (item == null) return null;
            var response = new viewmodels.Item
            {
                Id = item.Id,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                Status = item.Status,
                Version = item.Version,
                Manufacturer = item.Manufacturer,
                Name = item.Name,
                Price = item.Price
            };
            return response;
        }
    }
}
