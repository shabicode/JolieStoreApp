using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database.Query;
using System.Linq;
using Firebase.Database;
using System.Threading.Tasks;
using JolieStoreApp.Modelo;
using JolieStoreApp.Conexiones;

namespace JolieStoreApp.Datos
{
    public class Detallecompras
    {
        public async Task InsertarDc(DetalleCompra parametros)
        {
            await Conexion.firebase
                .Child("DetalleCompra")
                .PostAsync(new DetalleCompra()
                {
                    Cantidad = parametros.Cantidad,
                    Idproducto = parametros.Idproducto,
                    PrecioCompra = parametros.PrecioCompra,
                    Total = parametros.Total
                });
        }
        public async Task<List<DetalleCompra>> MostrarpreviaDc()
        {
            var ListaDc = new List<DetalleCompra>();
            var parametrosProductos = new ProductoModel();
            var funcionproductos = new Productos();
            var data = (await Conexion.firebase
                .Child("DetalleCompra")
                .OnceAsync<DetalleCompra>()) 
                .Select(item => new DetalleCompra
                {
                    Idproducto = item.Object.Idproducto,
                    IddetalleCompra = item.Key
                })
                ;

            foreach (var item in data)
            {
                var parametros = new DetalleCompra();
                parametros.Idproducto = item.Idproducto;
                parametrosProductos.Idproducto = item.Idproducto;
                var listaproductos = await funcionproductos.MostrarproductosById(parametrosProductos);
                parametros.Imagen = listaproductos[0].Icono;
                ListaDc.Add(parametros);
            }
            return ListaDc;
        }

    }
}
