using clientside.backend.Classes;
using clientside.backend.DIHelper;
using clientside.backend.Mappers;
using clientside.backend.Settings;
using RolDbContext;
using viewmodels;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class InfoService
    {
        public RolEfContext _context;
        private readonly ServiceSettings _serviceSettings;

        public InfoService(RolEfContext context, ServiceSettings serviceSettings) 
        {
            _context = context;
            _serviceSettings = serviceSettings;
        }
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
                    //TODO Return the "newest" object
                    return new ServiceResponse<viewmodels.Info>("En nyare version finns redan", Enums.ServiceResponseEnum.Conflict, info);
                }
                info.MapTo(oldItem);
                oldItem.UpdatedDate = DateTime.UtcNow;
                if(_serviceSettings.Mode == "Server")
                {
                    oldItem.Status = Status.SavedRemote;
                }
            }
            else
            {
                var item = info.Map()!;

                if (_serviceSettings.Mode == "Server")
                {
                    item.Status = Status.SavedRemote;
                }
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
