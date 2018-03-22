using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Common.Entities;
using WebStore.Common.Services;

namespace WebStore.Common.Transformers
{
    public  class CsvFileToDbTransformer
    {
        IEntityService<Product> repository;
        IList<Product> products;
        public CsvFileToDbTransformer(IEntityService<Product> repository, IList<Product> products)
        {
            this.repository = repository;
            this.products = products;
        }
        public void Execute()
        {
            foreach (var entity in products)
            {
                repository.Save(entity);
            }
        }
    }
}
