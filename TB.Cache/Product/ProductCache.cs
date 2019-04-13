using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Business.Product;
using TB.Business.Interfaces;

namespace TB.Cache.Product
{
    public class ProductCache
    {
        private const string cacheKey = "availableProducts";

        public IEnumerable GetAvailableProducts(string currentUser)
        {
            try
            {
                ObjectCache cache = MemoryCache.Default;
                if (cache.Contains(cacheKey))
                    return (IEnumerable)cache.Get(cacheKey);
                else
                {

                    IProduct productBO = new ProductBO(currentUser);
                    IEnumerable availableStocks = productBO.GetAllProducts();// this.GetDefaultStocks();

                    // Store data in the cache
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(6);
                    cache.Add(cacheKey, availableStocks, cacheItemPolicy);

                    return availableStocks;
                }
            
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
