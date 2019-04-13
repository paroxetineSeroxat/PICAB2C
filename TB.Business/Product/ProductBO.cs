using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Business.Interfaces;
using TB.Domain.BE;
using TB.Domain.Context;
using TB.Repository.Repositories;


namespace TB.Business.Product
{
    public class ProductBO : IProduct
    {
        TBContext context = new TBContext();
        private string currentUser = string.Empty;
        
        public ProductBO(string currentUser)
        {
            this.currentUser = currentUser;
        }

        public int CreateProduct(Domain.BE.Product product)
        {
            try
            {
                
                ProductRepository repo = new ProductRepository(context);
                repo.Add(product);
                return product.Id;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Domain.BE.Product GetProductsByCode(string code)
        {
            try
            {

                ProductRepository repo = new ProductRepository(context);
                return repo.FindByCode(code);//GetAll(x => x.ProductCode == code).FirstOrDefault();
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Domain.BE.Product> GetProductsByName(string name)
        {
            try
            {

                ProductRepository repo = new ProductRepository(context);
                if (string.IsNullOrEmpty(name))
                    return repo.GetAll();
                return repo.GetAll(x => x.ProductName.Contains(name) || x.ProductDescription.Contains(name));

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public Domain.BE.Product ProductDetail(int id)
        {
            try
            {

                ProductRepository repo = new ProductRepository(context);
                return repo.FindById(id);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Domain.BE.Product> TopSoldProductbyDate(DateTime starDate, DateTime endDate)
        {
            try
            {
                List<Domain.BE.Product> productList = new List<Domain.BE.Product>();

                PurchaseOrderRepository poRepo = new PurchaseOrderRepository(context);

                List<Domain.BE.PurchaseOrder> poList =  poRepo.GetAll(p => p.CreationDate >= starDate && p.CreationDate <= endDate);

                

                foreach (var po in poList)
                {
                    foreach (var pod in po.PurchaseOrderDetails)
                    {
                        if (!productList.Any(x => x.Id == pod.ProductId))
                            productList.Add(pod.Product);
                        else
                            productList.Find(x => x.Id == pod.ProductId).SoldQuantity += pod.Quantity;

                    }
                    
                }

                return productList;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool UpdateProduct(Domain.BE.Product product)
        {
            try
            {
                ProductRepository repo = new ProductRepository(context);
                repo.Update(product);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Domain.BE.Product> GetCampains()
        {
            try
            {

                ProductRepository repo = new ProductRepository(context);
                
                return repo.GetAll(x => x.IsCampain == true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public List<Domain.BE.Product> GetAllProducts()
        {
            try
            {

                ProductRepository repo = new ProductRepository(context);

                return repo.GetAll();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
