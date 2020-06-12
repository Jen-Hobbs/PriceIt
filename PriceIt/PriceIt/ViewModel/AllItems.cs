using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using PriceIt.Model;
using System.Threading.Tasks;
using System.Diagnostics;
using PriceIt.View;

namespace PriceIt.ViewModel
{
    class AllItems : BaseVM
    {
        DateTime dateTime;
        
        public ICommand Submit { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand DeleteAll { get; private set; }
        public string NameResult { get; private set; }
        public string AddName { get; set; }

        public string AddCategory { get; set;}



        private ObservableCollection<ItemDB> list;

        public ObservableCollection<ItemDB> Info
        {
            get { return list; }
            set { list = value; }
        }
        public AllItems()
        {


            Submit = new Command(DetermindResult);
            Delete = new Command(DeleteItem);
            DeleteAll = new Command(async()=> await DeleteEverything());
            AddDB();


        }
        public async Task DeleteEverything()
        {


            await App.Database.DeleteAll();
            await AddDB();
            OnPropertyChanged("Info");
        }
        public async void DeleteItem(object sender)
        {
            Debug.WriteLine(sender);
            foreach (ItemDB x in Info)
            {
                if (x.ID == (int)sender)
                {
                    await App.Database.DeleteItem(x);
                }
            }
            Console.WriteLine(sender);

            await AddDB();
            OnPropertyChanged("Info");
        }
        public async Task AddDB()
        {
            list = new ObservableCollection<ItemDB>();
            List<ItemDB> a = await App.Database.GetPeopleAsync();
            Debug.Write("add db");
            foreach (ItemDB temp in a)
            {
                list.Add(temp);
                Debug.Write(temp);
            }
            OnPropertyChanged("Info");
        }

        async void DetermindResult()
        {
            Debug.WriteLine("submit");

            await App.Database.SavePersonAsync(new ItemDB
            {
                Name = AddName,
                Category = AddCategory
            });
            await AddDB();

            MessagingCenter.Send<CategoryModel>(new CategoryModel
            {
                Title = AddCategory,
                TargetType = typeof(AllItems)
            }, "update");
            
        }
    


    }
}

