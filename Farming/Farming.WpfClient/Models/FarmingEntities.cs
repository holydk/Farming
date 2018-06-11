namespace Farming.WpfClient.Models
{
    public partial class FarmingEntities
    {
        public FarmingEntities(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public static FarmingEntities CreateManagerEntities() =>
            new FarmingEntities("name=FarmingManager");
    }
}
