using viewmodels;

namespace clientside.backend.Mappers
{
    public static class OrderRowMapper
    {
        public static viewmodels.OrderRow? Map(this RolDbContext.Models.OrderRow? orderRow)
        {
            if (orderRow == null) return null;
            var response = new viewmodels.OrderRow
            {
                Id = orderRow.Id,
                CreatedDate = orderRow.CreatedDate,
                UpdatedDate = orderRow.UpdatedDate,
                Ammount = orderRow.Ammount,
                Status = orderRow.Status,
                Version = orderRow.Version
            };
            return response;
        }
        public static RolDbContext.Models.OrderRow? Map(this viewmodels.OrderRow? orderRow)
        {
            if (orderRow == null) return null;
            var response = new RolDbContext.Models.OrderRow
            {
                Id = orderRow.Id ?? Guid.NewGuid(),
                CreatedDate = orderRow.CreatedDate,
                UpdatedDate = orderRow.UpdatedDate,
                Ammount = orderRow.Ammount,
                Status = orderRow.Status ?? Status.SavedLocal,
                Version = orderRow.Version
            };
            return response;
        }
    }
}
