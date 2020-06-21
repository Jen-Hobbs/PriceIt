using PriceIt.Model;
using PriceIt.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PriceIt.ViewModel
{
    public class Category : BaseVM
    {
        private ObservableCollection<CategoryModel> list;

        /// <summary>
        /// add all the categorys in database and subscribe to add new categories when an item is added
        /// </summary>
        public Category()
        {
            Pages();
            MessagingCenter.Subscribe<CategoryModel>(this, "update", (category)=>
            {
                Info.Add(category);
            });
            
        }

        /// <summary>
        /// All Categories displayed
        /// </summary>
        public ObservableCollection<CategoryModel> Info
        {
            get { return list; }
            set { list = value;
                OnPropertyChanged("Info");
            }
        }

        /// <summary>
        /// get all categories in database and add them to list
        /// </summary>
        /// <returns></returns>
        public async Task Pages()
        {
            list = new ObservableCollection<CategoryModel>();
            Info.Add(new CategoryModel
            {
                Title = "test",
                TargetType = typeof(Test)
            });
            Info.Add(new CategoryModel
            {
                Title = "All Items",
                TargetType = typeof(ItemList)
            });
            List<CategoryInfo> categories = await App.Database.GetCategoriesAsync();
            foreach (CategoryInfo a in categories)
            {
                Console.WriteLine(a.Category);
                Info.Add(new CategoryModel
                {
                    Title = a.Category,
                    TargetType = typeof(ItemList)
                });
            }


        }


    }
}
