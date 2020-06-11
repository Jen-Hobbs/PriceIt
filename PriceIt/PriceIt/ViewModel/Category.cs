using PriceIt.Model;
using PriceIt.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PriceIt.ViewModel
{
    public class Category : BaseVM
    {
        private static ObservableCollection<CategoryModel> list;
        public Category()
        {
            
            Pages();
            
        }
        public static ObservableCollection<CategoryModel> Info
        {
            get { Console.WriteLine("accessing list");  return list; }
            set { list = value; }
        }
        public async Task Pages()
        {
            list = new ObservableCollection<CategoryModel>();
            Console.WriteLine("test");
            
            list.Add(new CategoryModel
            {
                Title = "All Items",
                TargetType = typeof(ItemList)
            });
            List<CategoryInfo> categories = await App.Database.GetCategoriesAsync();
            foreach (CategoryInfo a in categories)
            {
                Console.WriteLine(a.Category);
                list.Add(new CategoryModel
                {
                    Title = a.Category,
                    TargetType = typeof(ItemList)
                });
            }
            OnPropertyChanged("Info");
            
        }

    }
}
