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
        public async Task<List<ProductoModel>> MostrarProductos()
        {
            return (await Conexion.firebase
                .Child("Productos").OnceAsync<ProductoModel>()).Select(item => new ProductoModel
                {
                    Descripcion = item.Object.Descripcion,
                    Icono = item.Object.Icono,
                    Precio = item.Object.Precio,
                    Peso = item.Object.Peso,
                    Idproducto = item.Key
                }).ToList();
        }
    }
}
