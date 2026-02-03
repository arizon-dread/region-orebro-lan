using clientside.backend.DIHelper;
using RolDbContext;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class OrderService
    {
        public RolDbContext.RolEfContext _context;
        public OrderService(RolDbContext.RolEfContext context) 
        {
            _context = context;
        }
    }
}
