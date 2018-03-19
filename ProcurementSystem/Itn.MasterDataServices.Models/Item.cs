
namespace Itn.MasterDataServices.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemNbr { get; set; }
        public string Description { get; set; }

        public static Item Create(int itemId,string itemnbr,string desc)
        {
            return new Item
            {
                Id = itemId,
                ItemNbr = itemnbr,
                Description = desc
            };
        }
    }
}
