using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class CustomerCaseItemRepository : RepositoryBase<CustomerCaseItem>
    {
        public CustomerCaseItem GetById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}