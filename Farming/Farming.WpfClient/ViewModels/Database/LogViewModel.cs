using System;
using System.ComponentModel;

namespace Farming.WpfClient.ViewModels.Database
{
    public class LogViewModel : ValidationViewModelBase
    {
        [Description("Идентификатор")]
        public int Id
        {
            get => Get(() => Id);
            set => Set(() => Id, value);
        }

        [Description("Название таблицы")]
        public string TableName
        {
            get => Get(() => TableName);
            set => Set(() => TableName, value);
        }

        [Description("Идентификатор в таблице")]
        public int? TableId
        {
            get => Get(() => TableId);
            set => Set(() => TableId, value);
        }

        [Description("Событие")]
        public string Event
        {
            get => Get(() => Event);
            set => Set(() => Event, value);
        }

        [Description("Дата")]
        public DateTime? Date
        {
            get => Get(() => Date);
            set => Set(() => Date, value);
        }

        public LogViewModel()
        {
            IsValidOnClick = true;

            AddRule(() => TableName, () => !string.IsNullOrWhiteSpace(TableName) && TableName.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => Event, () => !string.IsNullOrWhiteSpace(Event) && Event.Length < 50, "Поле не должно быть пустым и содержать больше 50 символов.");
            AddRule(() => Date, () => Date.HasValue, "Поле не должно быть пустым.");
            AddRule(() => TableId, () => TableId.HasValue && TableId > -1, "Поле не должно быть пустым и значение должно быть больше -1.");
        }
    }
}
