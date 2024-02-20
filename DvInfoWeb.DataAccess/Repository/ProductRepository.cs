using DvInfoWeb.DataAccess.Data;
using DvInfoWeb.DataAccess.Repository.IRepository;
using DvInfoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DvInfoWeb.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


    

        public void update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
