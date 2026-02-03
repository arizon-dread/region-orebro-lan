namespace clientside.backend.Mappers
{
    public static class Mappers
    {
        public static viewmodels.Info? Map(this RolDbContext.Models.Info? info)
         {
            if (info == null) return null;
            var response = new viewmodels.Info
            {

            };
            return response;
        }
    }
}
