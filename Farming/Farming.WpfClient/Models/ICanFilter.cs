namespace Farming.WpfClient.Models
{
    public interface ICanFilter
    {
        void ApplyFilter(params string[] by);

        void ClearFilter();
    }
}
