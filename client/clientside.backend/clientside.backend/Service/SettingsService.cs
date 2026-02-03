using clientside.backend.DIHelper;
using RolDbContext;
using viewmodels;

namespace clientside.backend.Service
{
    [Lifetime(Lifetime.Scoped)]
    public class SettingsService(RolEfContext context) 
    {
        public IEnumerable<ApplicationSettingsViewModel> GetSettings()
        {
            foreach(var item in context.ApplicationStatus)
            {
                yield return new ApplicationSettingsViewModel { Id = item.Id , Key = item.Key, Value = item.Value};
            }
        }
        public ApplicationSettingsViewModel? GetByKey(string k)
        {
            var settings= context.ApplicationStatus.FirstOrDefault(x => x.Key == k);
            if(settings == null) return null;
            return new ApplicationSettingsViewModel
            {
                Id = settings.Id,
                Key = settings.Key,
                Value = settings.Value
            };
        }
        public ApplicationSettingsViewModel Save(ApplicationSettingsViewModel model)
        {
            var old = context.ApplicationStatus.FirstOrDefault(d => d.Id == model.Id);
            if (old != null)
            {
                old.Key = model.Key;
                old.Value = model.Value;
                context.SaveChanges();
            }
            else
            {
                var newSettings = context.ApplicationStatus.Add(new RolDbContext.Models.ApplicationStatus { Key = model.Key, Value = model.Value });
                context.SaveChanges();
                model.Id = newSettings.Entity.Id;
            }
            return model;
        }
    }
}
