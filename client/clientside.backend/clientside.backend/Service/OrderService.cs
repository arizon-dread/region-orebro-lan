using clientside.backend.DIHelper;
using RolDbContext;
using RolDbContext.Models;

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
    }
}
