using System;
using System.Collections.Generic;

namespace RaalProjectPreview.DAL.Repository.Interface
{
    interface IRepository<T> where T : class
    {
        T Create(T item);
        T Delete(T item);
        List<T> GetList(Func<T, bool> query);
    }
}