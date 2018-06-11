using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Farming.WpfClient.Models;
using MaterialDesignThemes.Wpf;

namespace Farming.WpfClient.ViewModels.Database
{
    public class LogsViewModel : DatabaseTableViewModel<LogViewModel>
    {
        public LogsViewModel(ISnackbarMessageQueue snackbarMessageQueue, User user)
            : base("Логи", user, snackbarMessageQueue)
        {

        }

        protected async override Task UpdateAsync(FarmingEntities db)
        {
            var logs = await db.Logs.ToArrayAsync();

            foreach (var log in logs)
            {
                Models.Add(new LogViewModel()
                {
                    Id = log.Id,
                    TableName = log.TableName,
                    TableId = log.TableId,
                    Event = log.Event,
                    Date = log.Date
                });
            }
        }

        protected override Task AddAsync(FarmingEntities db, LogViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateAsync(FarmingEntities db, LogViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteAsync(FarmingEntities db, LogViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
