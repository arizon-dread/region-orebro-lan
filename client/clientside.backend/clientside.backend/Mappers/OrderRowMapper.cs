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
    }
}
