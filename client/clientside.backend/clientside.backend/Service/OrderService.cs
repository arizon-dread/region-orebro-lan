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
    public class OrderService(RolEfContext context, SettingsService settingsService)
    {
        private readonly RolEfContext _context = context;
        private readonly bool isServer = settingsService.IsServer;

        public ServiceResponse<viewmodels.Order> Delete(Guid id)
        {
            try
            {
                var order = _context.Order.FirstOrDefault(x => x.Id == id);
                if (order == null)
                {
                    return new ServiceResponse<viewmodels.Order>("Kunde inte hitta order med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                var orderRows = _context.OrderRow.Where(x => x.OrderId == id);
                _context.OrderRow.RemoveRange(orderRows);
                _context.Order.Remove(order);
                _context.SaveChanges();
                return new ServiceResponse<viewmodels.Order>("", ServiceResponseEnum.Deleted, null);
            }
            catch (Exception ex) { 
                return new ServiceResponse<viewmodels.Order>($"Kunde inte ta bort order med id: {id}. Exception: {ex.Message}", ServiceResponseEnum.Error, null);
            }
        }

        public ServiceResponse<viewmodels.Order> Get(Guid id)
        {
            try
            {
                var order = _context.Order.FirstOrDefault(x => x.Id == id);
                if (order == null) {
                    return new ServiceResponse<viewmodels.Order>("Kunde inte hitta order med id: " + id, ServiceResponseEnum.NotFound, null);
                }

                //Hämtar bara order, måste hämta customer och orderrows också.
                //Hade det funnits mer tid hade jag löst det annorlunda
                var orderItem = order.Map();
                //Hämta kund
                var customer = _context.Customer.FirstOrDefault(x => x.Id == order.CustomerId).Map();
                //var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == order.CustomerId.ToString().ToLower()).Map();
                if (customer == null)
                {
                    return new ServiceResponse<viewmodels.Order>("Kunde inte hitta kunden med id: " + order.CustomerId, ServiceResponseEnum.NotFound, null);
                }
                orderItem!.Customer = customer;
                //Hämta orderrader
                var orderRows = _context.OrderRow.Where(x => x.OrderId == order.Id);
                foreach (var row in orderRows)
                {
                    var item = _context.Item.FirstOrDefault(x => x.Id.ToString().ToLower() == row.ItemId.ToString().ToLower()).Map();
                    if (item == null)
                    {
                        return new ServiceResponse<viewmodels.Order>("Kunde inte hitta artikeln med id: " + row.ItemId, ServiceResponseEnum.NotFound, null);
                    }

                    var orderRow = row.Map();
                    if (orderRow == null) { 
                        return new ServiceResponse<viewmodels.Order>("Kunde inte hitta orderraden med id: " + row.Id, ServiceResponseEnum.NotFound, null);
                    }
                    row.ItemId = item.Id!.Value;
                    orderRow.Item = item!;
                    orderItem.OrderRows.Add(orderRow);
            }

            return new ServiceResponse<viewmodels.Order>("", ServiceResponseEnum.Success, orderItem);
            }
            catch (Exception ex) 
            {
                return new ServiceResponse<viewmodels.Order>("Kunde inte hämta order med id: " + id, ServiceResponseEnum.Error, null);
            }

        }
        private ServiceResponse<viewmodels.Order> Insert(viewmodels.Order order)
        {
            var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == order.Customer.Id.ToString().ToLower());
            if (customer == null)
            {
                //Kundens finns inte, är inte berättigad att beställa
                return new ServiceResponse<viewmodels.Order>("Kunden finnns inte. Id: " + order.Customer.Id, Enums.ServiceResponseEnum.Error, null);
            }
            order.Customer = customer.Map();

            var orderItem = order.Map()!;
            order.Version = orderItem.Version;
            order.Status = orderItem.Status;
            if (isServer)
            {
                order.Status = Status.SavedRemote;
                order.Version = order.Version + 1;
            }
            orderItem.Status = order.Status;
            orderItem.Version = order.Version;
            orderItem.CreatedDate = DateTime.UtcNow;
            orderItem.UpdatedDate = DateTime.UtcNow;
            _context.Order.Add(orderItem);
            order.Id = orderItem.Id;

            foreach (var row in order.OrderRows)
            {
                var item = _context.Item.FirstOrDefault(x => x.Id.ToString().ToLower() == row.Item.Id.ToString().ToLower());
                if (item == null) {
                    //Artikeln finns inte 
                    return new ServiceResponse<viewmodels.Order>("Kunde inte hitta artikeln. Id: " + row.Item.Id, Enums.ServiceResponseEnum.Error, null);
                }
                row.Item = item.Map()!;
                row.Status = Status.SavedLocal;
                row.Version = row.Version < 1 ? 1 : row.Version;
                //row.Item.Status = Status.SavedLocal;
                var rowItem = new RolDbContext.Models.OrderRow
                {
                    Id = row.Id ?? Guid.NewGuid(),
                    Ammount = row.Ammount,
                    CreatedDate = DateTime.UtcNow,
                    Version = row.Version < 1 ? 1 : row.Version,
                    ItemId = row.Item.Id!.Value,
                    OrderId = orderItem.Id,
                    UpdatedDate = DateTime.UtcNow,
                    Status = row.Status ?? Status.SavedLocal
                };
                if (isServer)
                {
                    order.Status = Status.SavedRemote;
                    order.Version = order.Version + 1;
                    row.Status = Status.SavedRemote;
                }
                //row.Item.Status = item.Status;
                //row.Item.Version = item.Version;
                _context.OrderRow.Add(rowItem);
                row.Id = rowItem.Id;
            }

            _context.SaveChanges();
            return new ServiceResponse<viewmodels.Order>("", Enums.ServiceResponseEnum.Success, order);
        }
        public ServiceResponse<viewmodels.Order> Update(viewmodels.Order order)
        {
            //VARFÖR BEHÖVER JAG KÖRA TOSTRING????
            var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == order.Customer.Id.ToString().ToLower());
            if (customer == null)
            {
                //Kundens finns inte, är inte berättigad att beställa
                return new ServiceResponse<viewmodels.Order>("Kunden finnns inte. Id: " + order.Customer.Id, Enums.ServiceResponseEnum.Error, null);
            }

            var orderItem = _context.Order.FirstOrDefault(d => d.Id.ToString().ToLower() == order.Id.ToString().ToLower());
            if (orderItem.Version > order.Version)
            {
                //TODO Return Conflict
                return new ServiceResponse<viewmodels.Order>($"Konflikt mellan orderversion. Befintlig version: {orderItem.Version}, ny version: {order.Version} ", Enums.ServiceResponseEnum.Conflict, null);
            }


            order.Version = orderItem.Version;
            order.Status = orderItem.Status;
            if (isServer)
            {
                order.Status = Status.SavedRemote;
                order.Version = order.Version + 1;
            }
            orderItem.Status = order.Status;
            orderItem.Version = order.Version;
            orderItem.UpdatedDate = DateTime.UtcNow;
            orderItem.DeliveryPostalCode = order.DeliveryPostalCode;
            orderItem.DeliveryAddress = order.DeliveryAddress;
            orderItem.DeliveryCity = order.DeliveryCity;
            orderItem.CustomerId = order.Customer.Id!.Value;

            //Quick and dirty...ta bort alla orderrader och lägg till dem som kommer i update
            var existingOrderRows = _context.OrderRow.Where(x => x.OrderId == order.Id);
            _context.OrderRow.RemoveRange(existingOrderRows);
            
            foreach (var row in order.OrderRows)
            {
                var rowItem = new RolDbContext.Models.OrderRow
                {
                    Id = Guid.NewGuid(),
                    Ammount = row.Ammount,
                    CreatedDate = DateTime.UtcNow,
                    Version = row.Version < 1 ? 1 : row.Version,
                    ItemId = row.Item.Id!.Value,
                    OrderId = orderItem.Id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (isServer)
                {
                    rowItem.Status = Status.SavedRemote;
                }
                row.Status = rowItem.Status;
                _context.OrderRow.Add(rowItem);
                row.Id = rowItem.Id;
            }
            _context.SaveChanges();
            return new ServiceResponse<viewmodels.Order>("", Enums.ServiceResponseEnum.Success, order);
        }

        public ServiceResponse<viewmodels.Order> Save(viewmodels.Order order)
        {
            var item = _context.Order.FirstOrDefault(d => d.Id.ToString().ToLower() == order.Id.ToString().ToLower());
            if (item != null)
            {
                return Update(order);
            }
            else
            {
                return Insert(order);
            }
        }
        public IEnumerable<viewmodels.Order> UpdatedSince(DateTime updateDate)
        {
            foreach (var item in _context.Order.Where(d => d.UpdatedDate > updateDate))
            {
                yield return item.Map()!;
            }
        }
        public ServiceResponse<List<viewmodels.Order>> GetSavedLocal()
        {
            var orderItems = new List<viewmodels.Order>();
            var orders = _context.Order.Where(d => d.Status == Status.SavedLocal).ToList();
            foreach (var order in orders)
            {
                var customer = _context.Customer.FirstOrDefault(d=>d.Id ==  order.CustomerId);
                if (customer == null)
                {
                    //No customer found
                    return new ServiceResponse<List<viewmodels.Order>>("Kunde inte hitta kunden med id: " + order.CustomerId, ServiceResponseEnum.NotFound, null);
                }
                var mapped = order.Map();
                mapped.Customer = customer.Map();
                orderItems.Add(mapped);
            }
            return new ServiceResponse<List<viewmodels.Order>>("", ServiceResponseEnum.Success, orderItems);
        }
        public ServiceResponse<List<viewmodels.Order>> GetAll()
        {
            var orders = new List<viewmodels.Order>();
            try
            {
                foreach (var order in _context.Order)
                {
                    var viewOrder = new viewmodels.Order
                    {
                        Id = order.Id,
                        Version = order.Version,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        DeliveryAddress = order.DeliveryAddress,
                        DeliveryCity = order.DeliveryCity,
                        DeliveryPostalCode = order.DeliveryPostalCode,
                        Status = order.Status
                    };

                    var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == order.CustomerId.ToString().ToLower());
                    if (customer == null)
                    {
                        //No customer found
                        viewOrder.Customer = new viewmodels.Customer { Name = "Kunde inte hitta kunden med id: " + order.CustomerId };
                    }
                    else
                    {
                        viewOrder.Customer = new viewmodels.Customer
                        {
                            Id = customer.Id,
                            Name = customer.Name,
                            CreatedDate = customer.CreatedDate,
                            UpdatedDate = customer.UpdatedDate,
                            Status = customer.Status,
                            Version = customer.Version,
                            Active = customer.Active,
                            DeliveryAddress = customer.DeliveryAddress,
                            DeliveryCity = customer.DeliveryCity,
                            DeliveryPostalCode = customer.DeliveryPostalCode
                        };
                    }
                    orders.Add(viewOrder);
                }
            } 
            catch (Exception ex) 
            {
                return new ServiceResponse<List<viewmodels.Order>>("Kunde inte hämta ordrar. Exception: " + ex.Message, Enums.ServiceResponseEnum.Error, null);
            }
            return new ServiceResponse<List<viewmodels.Order>>("", Enums.ServiceResponseEnum.Success, orders);

        }
    }
}
