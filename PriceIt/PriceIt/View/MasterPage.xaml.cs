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
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
            BindingContext = new PriceIt.ViewModel.Category();
        }
        /// <summary>
        /// Create page instance of items based on category
        /// Should be moved to viewmodel at somepoint using behaviours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Console.WriteLine("on item selected");
            var item = e.SelectedItem as CategoryModel;
            Console.WriteLine(item.Title);
            var masterDetail = App.Current.MainPage as MasterDetailPage;
            if (item != null)
            {
                masterDetail.Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, item));

                masterDetail.IsPresented = false;
            }
        }
    }
    
}