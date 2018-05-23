namespace Farming.WpfClient.Models
{
    public interface ICanSort
    {
        void ApplySort(SortType sortType, string value);

        void ClearSort();
    }
}
