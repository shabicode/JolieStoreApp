using JolieStoreApp.Conexiones;
using JolieStoreApp.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JolieStoreApp.Datos
{
    public class Productos
    {
        public async Task<List<ProductoModel>> Mostrarproductos()
        {
            try
            {
                return (await Conexion.firebase
                .Child("Productos")
                .OnceAsync<ProductoModel>()).Select(item => new ProductoModel
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Precio = item.Object.Precio,
                    Peso = item.Object.Peso,
                    Idproducto = item.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            
        }

        public async Task<List<ProductoModel>> MostrarproductosById(ProductoModel parametros)
        {
            try
            {
                return (await Conexion.firebase
                .Child("Productos")
                .OnceAsync<ProductoModel>()).Where(a=>a.Key == parametros.Idproducto).Select(item => new ProductoModel
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Precio = item.Object.Precio,
                    Peso = item.Object.Peso,
                    Idproducto = item.Key
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
