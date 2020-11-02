using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class CustomerCaseItemRepository : RepositoryBase<CustomerCaseItem>
    {
        public CustomerCaseItemRepository(DbContext context) : base(context)
        {

        }
        public CustomerCaseItem GetById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public void RemoveCaseByUserId(int userId)
        {
            List<CustomerCaseItem> customerCaseItems = (from caseItem in _DbSet
                                                        where caseItem.CustomerId == userId
                                                        select caseItem).ToList();
            foreach (var caseItem in customerCaseItems)
            {
                Delete(caseItem);
            }
        }
    }
}