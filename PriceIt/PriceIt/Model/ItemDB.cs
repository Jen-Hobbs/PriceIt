using SQLite;

namespace PriceIt.Model
{
    public class ItemDB
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }
    }
}
