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
    public class OrderService
    {
        public RolEfContext _context;
        public OrderService(RolEfContext context)
        {
            _context = context;
        }

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
                var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString() == order.CustomerId.ToString()).Map();
                if (customer == null)
                {
                    return new ServiceResponse<viewmodels.Order>("Kunde inte hitta kunden med id: " + order.CustomerId, ServiceResponseEnum.NotFound, null);
                }
                orderItem!.Customer = customer;
                //Hämta orderrader
                var orderRows = _context.OrderRow.Where(x => x.OrderId == order.Id);
                foreach (var row in orderRows)
                {
                    var orderRow = row.Map();
                    if (orderRow == null) { 
                        return new ServiceResponse<viewmodels.Order>("Kunde inte hitta orderraden med id: " + row.Id, ServiceResponseEnum.NotFound, null);
                    }
                    var item = _context.Item.FirstOrDefault(x => x.Id.ToString() == row.ItemId.ToString()).Map();
                    if (item == null)
                    {
                        return new ServiceResponse<viewmodels.Order>("Kunde inte hitta artikeln med id: " + item.Id, ServiceResponseEnum.NotFound, null);
                    }
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
            var customers = _context.Customer.ToList();

            var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString() == order.Customer.Id.ToString());
            if (customer == null)
            {
                //Kundens finns inte, är inte berättigad att beställa
                return new ServiceResponse<viewmodels.Order>("Kunden finnns inte. Id: " + order.Customer.Id, Enums.ServiceResponseEnum.Error, null);
            }

            var orderItem = new RolDbContext.Models.Order
            {
                Id = order.Id ?? Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Version = order.Version < 1 ? 1 : order.Version,
                CustomerId = order.Customer.Id!.Value,
                DeliveryAddress = order.DeliveryAddress,
                DeliveryCity = order.DeliveryCity,
                DeliveryPostalCode = order.DeliveryPostalCode,
                UpdatedDate = DateTime.UtcNow
            };
            _context.Order.Add(orderItem);
            order.Id = orderItem.Id;

            foreach (var row in order.OrderRows)
            {
                var item = _context.Item.FirstOrDefault(x => x.Id.ToString() == row.Item.Id.ToString());
                if (item == null) {
                    //Artikeln finns inte 
                    return new ServiceResponse<viewmodels.Order>("Kunde inte hitta artikeln. Id: " + row.Item.Id, Enums.ServiceResponseEnum.Error, null);
                }

                var rowItem = new RolDbContext.Models.OrderRow
                {
                    Id = row.Id ?? Guid.NewGuid(),
                    Ammount = row.Ammount,
                    CreatedDate = DateTime.UtcNow,
                    Version = row.Version < 1 ? 1 : row.Version,
                    ItemId = row.Item.Id!.Value,
                    OrderId = orderItem.Id,
                    UpdatedDate = DateTime.UtcNow
                };
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

            orderItem.DeliveryPostalCode = order.DeliveryPostalCode;
            orderItem.Version = order.Version;
            orderItem.UpdatedDate = DateTime.UtcNow;
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

                    var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString() == order.CustomerId.ToString());
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
                            Version = customer.Version

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
