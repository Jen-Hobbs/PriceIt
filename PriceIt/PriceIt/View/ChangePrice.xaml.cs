using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceIt.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;


namespace PriceIt.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePrice : PopupPage
    {
        public ChangePrice(ItemDB sender)
        {
            InitializeComponent();
            BindingContext = new PriceIt.ViewModel.ChangePriceVM(sender);
        }


    }
}