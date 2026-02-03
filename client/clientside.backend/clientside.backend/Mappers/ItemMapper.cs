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
        public static RolDbContext.Models.Item? Map(this viewmodels.Item? item)
        {
            if (item == null) return null;
            var response = new RolDbContext.Models.Item
            {
                Id = item.Id ?? Guid.NewGuid(),
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
        public static void MapTo(this viewmodels.Item vmItem, RolDbContext.Models.Item dbItem)
        {
            dbItem.Version = vmItem.Version;
            dbItem.Status = vmItem.Status;
            dbItem.Manufacturer = vmItem.Manufacturer;
            dbItem.Name = vmItem.Name;
            dbItem.Price = vmItem.Price;
        }

        public static void MapTo(this RolDbContext.Models.Item dbItem, viewmodels.Item vmItem)
        {
            vmItem.Id = dbItem.Id;
            vmItem.Version = dbItem.Version;
            vmItem.Status = dbItem.Status;
            vmItem.Manufacturer = dbItem.Manufacturer;
            vmItem.Name = dbItem.Name;
            vmItem.Price = dbItem.Price;
        }
    }
}
