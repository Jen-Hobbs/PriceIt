using SQLite;

namespace PriceIt.Model
{
    public class ItemDB
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public float MaxPrice { get; set; }

        public float MinPrice { get; set; }

        public string ItemWeightType { get; set; }
    }
}
