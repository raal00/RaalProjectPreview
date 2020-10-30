using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class ItemRepository : RepositoryBase<Item>
    {
        public Item GetItemById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public Item UpdateItem(Item item)
        {
            Item _updatedItem = GetItemById(item.Id);
            _updatedItem.Category = item.Category;
            _updatedItem.Code = item.Code;
            _updatedItem.Name = item.Name;
            _updatedItem.Price = item.Price;
            _APPContext.SaveChanges();
            return _updatedItem;
        }
    }
}