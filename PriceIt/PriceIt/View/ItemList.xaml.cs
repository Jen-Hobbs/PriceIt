using PriceIt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PriceIt.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemList : ContentPage
    {
        public ItemList()
        {
            InitializeComponent();
            BindingContext = new PriceIt.ViewModel.AllItems();
            Title = "All Items";
        }
        public ItemList(CategoryModel cat)
        {
            InitializeComponent();
            Title = cat.Title;
            BindingContext = new PriceIt.ViewModel.AllItems(cat);
        }
    }
}