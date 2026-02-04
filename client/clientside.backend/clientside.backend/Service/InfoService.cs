using clientside.backend.Classes;
using clientside.backend.DIHelper;
using clientside.backend.Mappers;
using RolDbContext;
using viewmodels;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class InfoService(RolEfContext context, SettingsService settingsService)
    {
        private readonly RolEfContext _context = context;
        private readonly bool isServer = settingsService.IsServer;

        public IEnumerable<viewmodels.Info> UpdatedSince (DateTime updateDate)
        {
            foreach(var item in _context.Info.Where(d => d.UpdatedDate > updateDate))
            {
                yield return item.Map()!;
            }
        }
        public IEnumerable<viewmodels.Info> GetSavedLocal()
        {
            foreach (var item in _context.Info.Where(d => d.Status == Status.SavedLocal))
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
                    return new ServiceResponse<viewmodels.Info>("En nyare version finns redan", Enums.ServiceResponseEnum.Conflict, oldItem.Map()!);
                }
                info.MapTo(oldItem);
                oldItem.UpdatedDate = DateTime.UtcNow;
                if (isServer)
                {
                    oldItem.Status = Status.SavedRemote;
                    oldItem.Version = info.Version + 1;
                }
                info = oldItem.Map()!;
            }
            else
            {
                var item = info.Map()!;
                if (isServer)
                {
                    item.Status = Status.SavedRemote;
                    item.Version = info.Version + 1;
                }
                item.CreatedDate = DateTime.UtcNow;
                item.UpdatedDate = DateTime.UtcNow;
                _context.Info.Add(item);
                info = item.Map()!;
                
            }
            _context.SaveChanges();
            return new ServiceResponse<viewmodels.Info>("", Enums.ServiceResponseEnum.Success, info);
        }
    }
}
