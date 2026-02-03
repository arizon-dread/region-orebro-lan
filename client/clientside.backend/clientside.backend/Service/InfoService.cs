using clientside.backend.Classes;
using clientside.backend.DIHelper;
using clientside.backend.Mappers;
using RolDbContext;
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
        public IEnumerable<viewmodels.Info> UpdatedSince (DateTime updateDate)
        {
            foreach(var item in _context.Info.Where(d => d.UpdatedDate > updateDate))
            {
                yield return item.Map()!;
            }
        }
        public IEnumerable<viewmodels.Info> All() 
        {
            var items = _context.Info;
            foreach (var item in items.OrderBy(d => d.PublishDate))
            {
                yield return item.Map()!;
            }
        }
        public IEnumerable<viewmodels.Info> Active()
        {
            var items = _context.Info;
            foreach (var item in items.Where(d => d.Unpublished == null).OrderBy(d => d.PublishDate))
            {
                yield return item.Map()!;
            }
        }

        public ServiceResponse<viewmodels.Info> Save(viewmodels.Info info)
        {
            var oldItem = _context.Info.FirstOrDefault(d => d.Id == info.Id);
            if (oldItem != null)
            {
                if (oldItem.Version > info.Version)
                {
                    //TODO Return the "newest" object
                    return new ServiceResponse<viewmodels.Info>("En nyare version finns redan", Enums.ServiceResponseEnum.Conflict, info);
                }
                info.MapTo(oldItem);
                oldItem.Status = Status.SavedLocal;
                oldItem.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                var item = info.Map()!;
                item.Status = Status.SavedLocal;
                item.CreatedDate = DateTime.UtcNow;
                item.UpdatedDate = DateTime.UtcNow;
                _context.Info.Add(item);
                info.Id = item.Id;
            }
            _context.SaveChanges();
            return new ServiceResponse<viewmodels.Info>("", Enums.ServiceResponseEnum.Success, info);
        }
    }
}
