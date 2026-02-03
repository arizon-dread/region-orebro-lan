namespace clientside.backend.Mappers
{
    public static class InfoMapper
    {
        public static viewmodels.Info? Map(this RolDbContext.Models.Info? info)
         {
            if (info == null) return null;
            var response = new viewmodels.Info
            {
                Id = info.Id,
                Text = info.Text,
                Title   = info.Title,
                PublishDate = info.PublishDate,
                Unpublished = info.Unpublished,
                Version = info.Version,
                Status = info.Status,
            };
            return response;
        }

        public static void Map(RolDbContext.Models.Info dbinfo,viewmodels.Info vminfo )
        {
            dbinfo.Text = vminfo.Text;
            dbinfo.Title = vminfo.Title;
            dbinfo.Version = vminfo.Version;
            dbinfo.PublishDate = vminfo.PublishDate ?? DateTime.Now ;
            dbinfo.Unpublished = vminfo.Unpublished;
            dbinfo.Status = vminfo.Status;
        }
        public static void MapTo(this RolDbContext.Models.Info dbinfo, viewmodels.Info vminfo)
        {
            dbinfo.Text = vminfo.Text;
            dbinfo.Title = vminfo.Title;
            dbinfo.Version = vminfo.Version;
            dbinfo.PublishDate = vminfo.PublishDate ?? DateTime.Now;
            dbinfo.Unpublished = vminfo.Unpublished;
            dbinfo.Status = vminfo.Status;
        }

        public static void MapTo(this  viewmodels.Info vminfo, RolDbContext.Models.Info dbinfo)
        {
            vminfo.Id = dbinfo.Id;
            vminfo.Text = dbinfo.Text;
            vminfo.Title = dbinfo.Title;
            vminfo.Version = dbinfo.Version;
            vminfo.PublishDate = dbinfo.PublishDate;
            vminfo.Unpublished = dbinfo.Unpublished;
            vminfo.Status = dbinfo.Status;
        }

        public static void Map(viewmodels.Info vminfo, RolDbContext.Models.Info dbinfo)
        {
            vminfo.Id = dbinfo.Id;
            vminfo.Text = dbinfo.Text;
            vminfo.Title = dbinfo.Title;
            vminfo.Version = dbinfo.Version;
            vminfo.PublishDate = dbinfo.PublishDate;
            vminfo.Unpublished = dbinfo.Unpublished;
            vminfo.Status = dbinfo.Status;
        }
        public static  RolDbContext.Models.Info? Map(this viewmodels.Info? info)
        {
            if (info == null) return null;
            var response = new RolDbContext.Models.Info
            {
                Id = info.Id ?? Guid.NewGuid(),
                Text = info.Text,
                Title = info.Title,
                PublishDate = info.PublishDate ?? DateTime.Now,
                Unpublished = info.Unpublished,
                Version = info.Version < 1 ? 1 : info.Version
            };
            return response;
        }

    }
}
