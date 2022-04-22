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
    public partial class Compras : ContentPage
    {
        VMCompras vm;
        public Compras()
        {
            InitializeComponent();
            vm = new VMCompras(Navigation, CarrilDerecha, CarrilIzquierda);
            BindingContext = vm;
            this.Appearing += Compras_Appearing;
        }

        private async void Compras_Appearing(object sender, EventArgs e)
        {
           await vm.MostrarVistapreviaDC();
        }
    }
}