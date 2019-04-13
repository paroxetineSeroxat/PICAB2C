using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.BE;
namespace TB.Business.Interfaces
{
    public interface IProduct
    {
        //Buscar productos por código 
        Domain.BE.Product GetProductsByCode(string code);
        //Buscar productos por nombre utilizando comodine
        //Buscar productos por descripción utilizando comodines .
        List<Domain.BE.Product> GetProductsByName(string name);
        //Tener en cuenta el detalle de productos se debe mostrar 
        Domain.BE.Product ProductDetail(int id);
        //El sistema permitirá la creación, modificación y eliminación de los productos TouresBalón/KS. (Incluye
        //la administración de imágenes). 
        // El sistema permitirá creación, modificación y eliminación de campañas .
        int CreateProduct(Domain.BE.Product product);
        //El sistema permitirá la creación, modificación y eliminación de los productos TouresBalón/KS. (Incluye
        //la administración de imágenes). (no se eliminan se cam,bian de estado)
        bool UpdateProduct(Domain.BE.Product product);
        //Ranking de los productos más vendidos en un rango de fechas dado, es decir, los productos ordenados
        //desde el que más ha participado en órdenes de pedido.
        List<Domain.BE.Product> TopSoldProductbyDate(DateTime starDate, DateTime endDate);

        //NO TENEMOS CATEGORIAS AUN, no se sabe que es
        //Ranking de las categorías más vendidas en un rango de fechas dado, es decir, las categorías ordenadas
        //desde la que más ha participado en órdenes de pedido.
        List<Domain.BE.Product> GetCampains();

        List<Domain.BE.Product> GetAllProducts();





    }
}
