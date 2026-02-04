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
    public class CustomerService
    {
        public RolEfContext _context;
        public CustomerService(RolEfContext context)
        {
            _context = context;
        }
        public IEnumerable<viewmodels.Customer> UpdatedSince(DateTime updateDate)
        {
            foreach (var customer in _context.Customer.Where(d => d.UpdatedDate > updateDate))
            {
                yield return customer.Map()!;
            }
        }
        public ServiceResponse<viewmodels.Customer> Delete(Guid id)
        {
            try
            {
                var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
                if (customer == null)
                {
                    return new ServiceResponse<viewmodels.Customer>("Kunde inte hitta beställare med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                _context.Customer.Remove(customer);
                _context.SaveChanges();
                return new ServiceResponse<viewmodels.Customer>("", ServiceResponseEnum.Deleted, null);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<viewmodels.Customer>($"Kunde inte ta bort beställare med id: {id}. Exception: {ex.Message}", ServiceResponseEnum.Error, null);
            }
        }

        public ServiceResponse<viewmodels.Customer> Get(Guid id)
        {
            try
            {
                var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
                if (customer == null)
                {
                    return new ServiceResponse<viewmodels.Customer>("Kunde inte hitta beställare med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                var customerRow = customer.Map();


                //Hämta ordrar men inte rader för översikt av beställare
                var orders = _context.Order.Where(x => x.CustomerId == id);
                foreach (var order in orders)
                {
                    customerRow!.Orders.Add(order.Map()!);
                }

                return new ServiceResponse<viewmodels.Customer>("", ServiceResponseEnum.Success, customerRow);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<viewmodels.Customer>("Kunde inte hämta beställare med id: " + id, ServiceResponseEnum.Error, null);
            }

        }

        public ServiceResponse<viewmodels.Customer> Save(viewmodels.Customer customer)
        {
            var oldItem = _context.Customer.FirstOrDefault(d => d.Id.ToString().ToLower() == customer.Id.ToString().ToLower());
            if (oldItem != null)
            {
                if (oldItem.Version > customer.Version)
                {
                    return new ServiceResponse<viewmodels.Customer>("En nyare version finns redan", Enums.ServiceResponseEnum.Conflict, customer);
                }
                customer.MapTo(oldItem);
                oldItem.Status = Status.SavedLocal;
                oldItem.UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                oldItem = customer.Map()!;
                oldItem.Status = Status.SavedLocal;
                oldItem.CreatedDate = DateTime.UtcNow;
                oldItem.UpdatedDate = DateTime.UtcNow;
                _context.Customer.Add(oldItem);
            }
            customer = oldItem.Map()!;
            _context.SaveChanges();
            return new ServiceResponse<viewmodels.Customer>("", Enums.ServiceResponseEnum.Success, customer);

        }
        public ServiceResponse<List<viewmodels.Customer>> GetAll()
        {
            var customers = new List<viewmodels.Customer>();
            try
            {
                foreach (var customer in _context.Customer)
                {
                    var viewCustomer = customer.Map();
                    customers.Add(viewCustomer);
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<viewmodels.Customer>>("Kunde inte hämta beställare. Exception: " + ex.Message, Enums.ServiceResponseEnum.Error, null);
            }
            return new ServiceResponse<List<viewmodels.Customer>>("", Enums.ServiceResponseEnum.Success, customers);

        }

        public ServiceResponse<viewmodels.Customer> ToggleActivation(Guid id)
        {
            try
            {
                var customer = _context.Customer.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
                if (customer == null)
                {
                    return new ServiceResponse<viewmodels.Customer>("Kunde inte hitta beställare med id: " + id, ServiceResponseEnum.NotFound, null);
                }
                customer.Active = !customer.Active;
                var customerRow = customer.Map();
                _context.SaveChanges();
                return new ServiceResponse<viewmodels.Customer>("", Enums.ServiceResponseEnum.Success, customerRow);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<viewmodels.Customer>("Kunde inte byta aktiv status för beställare. Exception: " + ex.Message, Enums.ServiceResponseEnum.Error, null);
            }
        }
    }
}
