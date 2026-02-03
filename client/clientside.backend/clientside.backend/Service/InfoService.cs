using clientside.backend.Classes;
using clientside.backend.DIHelper;
using RolDbContext;
using RolDbContext.Models;
using viewmodels;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class InfoService
    {
        public RolEfContext _context;
        public InfoService(RolEfContext context) 
        {
            _context = context;
        }
        public IEnumerable<viewmodels.Info> All() 
        {
            var items = _context.Info;
            foreach (var item in items)
            {
                yield return new viewmodels.Info
                {

                    Id = item.Id,
                    PublishDate = item.PublishDate,
                    Unpublished = item.Unpublished,
                    Text = item.Text,
                    Title = item.Title,
                    Version = item.Version,
                };
            }
        }
        public IEnumerable<viewmodels.Info> Active()
        {
            var items = _context.Info;
            foreach (var item in items.Where(d => d.Unpublished == null))
            {
                yield return new viewmodels.Info
                {

                    Id = item.Id,
                    PublishDate = item.PublishDate,
                    Unpublished = item.Unpublished,
                    Text = item.Text,
                    Title = item.Title,
                    Version = item.Version,
                };
            }
        }

        public void Save(viewmodels.Info info)
        {
            var oldItem = _context.Info.FirstOrDefault(d => d.Id == info.Id);
            Info? item = null;
            if (oldItem != null)
            {
                if(oldItem.Version > info.Version)
                {
                    //TODO Return Conflict
                }
                oldItem.Unpublished = info.Unpublished;
                oldItem.Text = info.Text;
                oldItem.Title = info.Title;
                oldItem.Version = info.Version;
                item = oldItem;

            }
            else
            {
                item = new Info
                {
                   
                    Id = info.Id ?? Guid.NewGuid(),
                    PublishDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Text = info.Text,
                    Title = info.Title,
                    Version = info.Version < 1 ? 1 : info.Version,
                };
                _context.Info.Add(item);
            }
            _context.SaveChanges();
            return new ServiceResponse<Info>("", Enums.ServiceResponseEnum.Success, item);
        }
    }
}
