using JolieStoreApp.Modelo;
using JolieStoreApp.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JolieStoreApp.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agregarcompra : ContentPage
    {
        public Agregarcompra(ProductoModel parametrosTrae)
        {
            InitializeComponent();
            BindingContext = new VMAgregarCompra(Navigation, parametrosTrae);
        }
    }
}