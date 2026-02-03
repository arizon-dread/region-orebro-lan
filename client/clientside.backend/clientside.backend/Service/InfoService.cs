using clientside.backend.DIHelper;
using RolDbContext;
using RolDbContext.Models;

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
        public void Save(viewmodels.Info info)
        {

            var oldItem = _context.Info.FirstOrDefault(d => d.Id == info.Id);
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

            }
            else
            {
                var item = new Info
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
        }
    }
}
