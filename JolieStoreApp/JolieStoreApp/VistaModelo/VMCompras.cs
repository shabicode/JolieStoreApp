using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using JolieStoreApp.Modelo;
using JolieStoreApp.Datos;
using System.Collections.Generic;
using JolieStoreApp.Vistas;
using Plugin.SharedTransitions;

namespace JolieStoreApp.VistaModelo
{
    public class VMCompras : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        private List<ProductoModel> _listaProductos;
        private List<DetalleCompra> _listaVistaPreviaDc;
        int _index;
        #endregion
        #region CONSTRUCTOR
        public VMCompras(INavigation navigation, StackLayout CarrilDerecha, StackLayout CarrilIzquierda)
        {
            Navigation = navigation;
            MostrarProductos(CarrilDerecha,CarrilIzquierda);
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        public List<ProductoModel> ListaProductos
        {
            get { return _listaProductos; }
            set { SetValue(ref _listaProductos, value); }
        }
        public List<DetalleCompra> ListaVistaPreviaDc
        {
            get { return _listaVistaPreviaDc; }
            set { SetValue(ref _listaVistaPreviaDc, value); }
        }
        
        #endregion
        #region PROCESOS
        public async Task MostrarProductos(StackLayout CarrilDerecha, StackLayout CarrilIzquierda)
        {
            var funncion = new Productos();
            ListaProductos = await funncion.Mostrarproductos();
            var box = new BoxView
            {
                HeightRequest = 40
            };
            CarrilIzquierda.Children.Clear();
            CarrilDerecha.Children.Clear();
            CarrilDerecha.Children.Add(box);
            foreach(var item in ListaProductos)
            {
                DibujarProductos(item, _index, CarrilDerecha, CarrilIzquierda);
                _index++;
            }
        }
        public void DibujarProductos(ProductoModel item, int index, StackLayout CarrilDerecha, StackLayout CarrilIzquierda)
        {
            var _ubicacion = Convert.ToBoolean(index % 2);
            var _carril = _ubicacion ? CarrilDerecha : CarrilIzquierda;

            var frame = new Frame
            {
                HeightRequest = 300,
                CornerRadius = 10,
                Margin = 8,
                HasShadow = false,
                BackgroundColor = Color.White,
                Padding = 22
            };

            var stack = new StackLayout
            {

            };
            var image = new Image
            {
                HeightRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10),
                Source = item.Icono,
            };
            var labelprecio = new Label
            {
                Text = "$" + item.Precio,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                Margin = new Thickness(0, 10),
                TextColor = Color.FromHex("#333333")
            };
            var labeldescripcion = new Label
            {
                Text = item.Descripcion,
                FontSize = 10,
                TextColor = Color.Black,
                CharacterSpacing = 1
            };
            var labelpeso = new Label
            {
                Text = item.Peso + "g",
                FontSize = 13,
                TextColor = Color.FromHex("#CCCCCC"),
                CharacterSpacing = 1
            };
            stack.Children.Add(image);
            stack.Children.Add(labelprecio);
            stack.Children.Add(labeldescripcion);
            stack.Children.Add(labelpeso);

            frame.Content = stack;
            var tap = new TapGestureRecognizer();
            tap.Tapped += async (object sender, EventArgs e) =>
            {
                var page = (App.Current.MainPage as SharedTransitionNavigationPage).CurrentPage;
                SharedTransitionNavigationPage.SetBackgroundAnimation(page, BackgroundAnimation.SlideFromRight);
                SharedTransitionNavigationPage.SetTransitionDuration(page, 1000);
                SharedTransitionNavigationPage.SetTransitionSelectedGroup(page, item.Idproducto);
                await Navigation.PushAsync(new Agregarcompra(item));
            };
            _carril.Children.Add(frame);
            stack.GestureRecognizers.Add(tap);
        }
        public async Task ProcesoAsyncrono()
        {

        }
        public async Task MostrarVistapreviaDC()
        {
            var funcion = new Detallecompras();
            ListaVistaPreviaDc = await funcion.MostrarpreviaDc();
        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
