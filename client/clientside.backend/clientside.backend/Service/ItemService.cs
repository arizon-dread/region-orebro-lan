using clientside.backend.Classes;
using clientside.backend.DIHelper;
using clientside.backend.Enums;
using clientside.backend.Mappers;
using Microsoft.EntityFrameworkCore;
using RolDbContext;
using RolDbContext.Models;
using SQLitePCL;
using viewmodels;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class ItemService
    {
        public RolEfContext _context;
        public ItemService(RolEfContext context)
        {
            _context = context;
        }
        public IEnumerable<viewmodels.Item> UpdatedSince(DateTime updateDate)
        {
            foreach (var item in _context.Item.Where(d => d.UpdatedDate > updateDate))
            {
                yield return item.Map()!;
            }
        }
        public ServiceResponse<viewmodels.Item> Delete(Guid id)
        {
            try
            {
                var item = _context.Item.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
                if (item == null)
                {
                    return new ServiceResponse<viewmodels.Item>("Kunde inte hitta artikel med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                _context.Item.Remove(item);
                _context.SaveChanges();
                return new ServiceResponse<viewmodels.Item>("", ServiceResponseEnum.Deleted, null);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<viewmodels.Item>($"Kunde inte ta bort artikel med id: {id}. Exception: {ex.Message}", ServiceResponseEnum.Error, null);
            }
        }

        public ServiceResponse<viewmodels.Item> Get(Guid id)
        {
            try
            {
                var item = _context.Item.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
                if (item == null)
                {
                    return new ServiceResponse<viewmodels.Item>("Kunde inte hitta artikel med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                var itemRow = item.Map();
                return new ServiceResponse<viewmodels.Item>("", ServiceResponseEnum.Success, itemRow);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<viewmodels.Item>("Kunde inte hämta artikel med id: " + id, ServiceResponseEnum.Error, null);
            }

        }

        public ServiceResponse<viewmodels.Item> Save(viewmodels.Item item)
        {
            var oldItem = _context.Item.FirstOrDefault(d => d.Id.ToString().ToLower() == item.Id.ToString().ToLower());
            if (oldItem != null)
            {
                if (oldItem.Version > item.Version)
                {
                    return new ServiceResponse<viewmodels.Item>("En nyare version finns redan", Enums.ServiceResponseEnum.Conflict, item);
                }
                item.MapTo(oldItem);
                oldItem.Status = Status.SavedLocal;
                oldItem.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                oldItem = item.Map()!;
                oldItem.Status = Status.SavedLocal;
                oldItem.CreatedDate = DateTime.UtcNow;
                oldItem.UpdatedDate = DateTime.UtcNow;
                _context.Item.Add(oldItem);
            }
            item = oldItem.Map()!;
            _context.SaveChanges();
            return new ServiceResponse<viewmodels.Item>("", Enums.ServiceResponseEnum.Success, item);

        }
        public ServiceResponse<List<viewmodels.Item>> GetAll()
        {
            var items = new List<viewmodels.Item>();
            try
            {
                foreach (var item in _context.Item)
                {
                    var viewItem = item.Map();
                    items.Add(viewItem);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<viewmodels.Item>>("Kunde inte hämta artiklar. Exception: " + ex.Message, Enums.ServiceResponseEnum.Error, null);
            }
            return new ServiceResponse<List<viewmodels.Item>>("", Enums.ServiceResponseEnum.Success, items);

        }
        public ServiceResponse<viewmodels.Item> ToggleActivation(Guid id)
        {
            try
            {
                var item = _context.Item.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
                if (item == null)
                {
                    return new ServiceResponse<viewmodels.Item>("Kunde inte hitta artikel med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                item.Active = !item.Active;
                var itemRow = item.Map();
                _context.SaveChanges();
                return new ServiceResponse<viewmodels.Item>("", Enums.ServiceResponseEnum.Success, itemRow);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<viewmodels.Item>("Kunde inte byta aktiv status för artikel. Exception: " + ex.Message, Enums.ServiceResponseEnum.Error, null);
            }
        }
    }
}
