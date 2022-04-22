using JolieStoreApp.Datos;
using JolieStoreApp.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JolieStoreApp.VistaModelo
{
    public class VMAgregarCompra:BaseViewModel
    {

        #region VARIABLES
        int _Cantidad;
        public ProductoModel Parametrosrecibe { get; set; }
        string _PrecioTexto;
        #endregion
        #region CONSTRUCTOR
        public VMAgregarCompra(INavigation navigation,ProductoModel parametrosTrae)
        {
            Navigation = navigation;
            Parametrosrecibe = parametrosTrae;
            PrecioTexto = "$" + Parametrosrecibe.Precio;
        }
        #endregion
        #region OBJETOS
        public int Cantidad
        {
            get { return _Cantidad; }
            set { SetValue(ref _Cantidad, value); }
        } 
        public string PrecioTexto
        {
            get { return _PrecioTexto; }
            set { SetValue(ref _PrecioTexto, value); }
        }
        #endregion
        #region PROCESOS
        public async Task InsertarDc()
        {
            if(Cantidad == 0)
            {
                Cantidad = 1;
            } 
            var funcion = new Detallecompras();
            var parametros = new DetalleCompra();
            parametros.Cantidad = Cantidad.ToString();
            parametros.Idproducto = Parametrosrecibe.Idproducto;
            parametros.PrecioCompra = Parametrosrecibe.Precio;
            double total = 0;
            double preciocompra = Convert.ToDouble(Parametrosrecibe.Precio);
            total = Cantidad * preciocompra;
            parametros.Total = total.ToString();
            await funcion.InsertarDc(parametros);
            await Volver();
        }
        public async Task Volver()
        {
            await Navigation.PopAsync();
        }
        public void Aumentar()
        {
            Cantidad += 1;
        }
        public void Disminuye()
        {
            if (Cantidad > 0)
            {
                Cantidad -= 1;
            }
        }
        #endregion
        #region COMANDOS
        public ICommand VolverCommand => new Command(async () => await Volver());
        public ICommand AumentarCommand => new Command(Aumentar);
        public ICommand DisminuirCommand => new Command(Disminuye);
        public ICommand InsertarCommand => new Command(async () => await InsertarDc());
        #endregion
    }
}
