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
        }
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Console.WriteLine("on item selected");
            var item = e.SelectedItem as CategoryModel;
            var masterDetail = App.Current.MainPage as MasterDetailPage;
            if (item != null)
            {
                masterDetail.Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));

                masterDetail.IsPresented = false;
            }
        }
    }
    
}