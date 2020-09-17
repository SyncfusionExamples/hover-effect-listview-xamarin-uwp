using ListViewXamarin;
using ListViewXamarin.UWP;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomGrid), typeof(CustomGridRenderer))]
namespace ListViewXamarin.UWP
{
    #region CustomGridRenderer
    public class CustomGridRenderer : VisualElementRenderer<CustomGrid, FrameworkElement>
    {
        #region Constructor
        public CustomGridRenderer()
        {
            this.PointerEntered += CustomGridRenderer_PointerEntered;
            this.PointerExited += CustomGridRenderer_PointerExited;
            this.PointerPressed += CustomGridRenderer_PointerPressed;
        }
        #endregion

        #region Callbacks
        private void CustomGridRenderer_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = this.Element;
            item.BackgroundColor = Xamarin.Forms.Color.FromHex("#a6dcef");
        }

        private void CustomGridRenderer_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = this.Element;
            item.BackgroundColor = Xamarin.Forms.Color.Transparent;
        }

        private void CustomGridRenderer_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = this.Element;
            var itemData = item.BindingContext as Contacts;
            if (itemData.IsSelected)
                item.BackgroundColor = Xamarin.Forms.Color.Transparent;
            else
                item.BackgroundColor = Xamarin.Forms.Color.WhiteSmoke;
        }
        #endregion
    }
    #endregion
}

