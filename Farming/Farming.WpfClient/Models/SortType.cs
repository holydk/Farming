using System.ComponentModel;

namespace Farming.WpfClient.Models
{
    public enum SortType
    {
        [Description("По возрастанию")]
        Ascending,

        [Description("По убыванию")]
        Descending
    }
}
