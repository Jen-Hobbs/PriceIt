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
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

/// <summary>
/// TODO: Items can have multiple categories.
/// </summary>
namespace PriceIt.ViewModel
{
    class AllItems : BaseVM
    {
        
        /// <summary>
        /// Command used when user submits a new item
        /// </summary>
        public ICommand Submit { get; private set; }
        /// <summary>
        /// command used to delete an item
        /// </summary>
        public ICommand Delete { get; private set; }
        /// <summary>
        /// delete all items
        /// </summary>
        public ICommand DeleteAll { get; private set; }

        /// <summary>
        /// Open popup to edit price of time
        /// </summary>
        public ICommand Edit { get; private set; }

        /// <summary>
        /// hide the item submission form
        /// </summary>
        public ICommand HideForm { get; private set; }

        private string _addName;
        /// <summary>
        /// Add name Binding for adding item
        /// </summary>
        public string AddName
        {
            get
            {
                return _addName;
            }
            set
            {
                _addName = value;
                OnPropertyChanged("AddName");
            }
        }

        private string _addPrice;

        /// <summary>
        /// Add price binding for adding item
        /// </summary>
        public string AddPrice {
            get 
            {
                return _addPrice; 
            } 
            set 
            {
                _addPrice = value;
                OnPropertyChanged("AddPrice");

            } 
        }
        private string _addCategory;
        /// <summary>
        /// Add category for adding item
        /// </summary>
        public string AddCategory
        {
            get
            {
                return _addCategory;
            }
            set
            {
                _addCategory = value;
                OnPropertyChanged("AddCategory");

            }
        }

        private string _pageCategory;

        /// <summary>
        /// Current category for adding item
        /// Will also contain the current page category
        /// </summary>
        public string PageCategory
        {
            get
            {
                return _pageCategory;
            }
            set
            {
                _pageCategory = value;
                OnPropertyChanged("PageCategory");

            }
        }

        private string _addMaxPrice;

        /// <summary>
        /// Add Max Price for form
        /// </summary>
        public string AddMaxPrice
        {
            get
            {
                return _addMaxPrice;
            }
            set
            {
                _addMaxPrice = value;
                OnPropertyChanged("AddMaxPrice");
            }
        }
        private string _addItemWeightType;

        /// <summary>
        /// Add Weight type for adding item
        /// </summary>
        public string AddItemWeightType
        {
            get
            {
                return _addItemWeightType;
            }
            set
            {
                _addItemWeightType = value;
                OnPropertyChanged("AddItemWeightType");
                
            }
        }
        
        
        private string categoryTitle;

        private bool _setVisable;

        /// <summary>
        /// True: Adding items are visable
        /// False: adding items form is invisible
        /// </summary>

        public bool SetVisable
        {
            get
            {
                return _setVisable;
            }
            set
            {
                _setVisable = value;
                OnPropertyChanged("SetVisable");
            }
        }
        
        /// <summary>
        /// set navigation for the page
        /// </summary>
        public INavigation Navigation { get; set; }

        
        private ObservableCollection<ItemDB> list;

        /// <summary>
        /// List of all items in db Will update page when item is added
        /// </summary>
        public ObservableCollection<ItemDB> Info
        {
            get { return list; }
            set { 
                list = value;
                OnPropertyChanged("Info");
            }
        }

        /// <summary>
        /// Used for the constructor to start all the commands needed for the all items page
        /// </summary>
        private void initiateCommands()
        {
            Submit = new Command(AddItem);
            Delete = new Command(DeleteItem);
            DeleteAll = new Command(async () => await DeleteEverything());
            Edit = new Command(EditPrice);
            SetVisable = false;
            HideForm = new Command(HideFormFromView);
        }
        /// <summary>
        /// constructor adds all the commands sets the page category and adds the items to the page
        /// </summary>
        public AllItems()
        {
            initiateCommands();

            categoryTitle = null;
            AddDB();
            PriceIsChanged();
            PageCategory = "Category";
        }
        /// <summary>
        /// constructor adds all the commands sets the page category and adds the items to the page
        /// </summary>
        /// <param name="cat">The category of the items pulled</param>
        public AllItems(CategoryModel cat)
        {
            initiateCommands();
            categoryTitle = null;
            PageCategory = "Category";
            if(!cat.Title.Equals("All Items"))
            {
                categoryTitle = cat.Title;
                PageCategory = cat.Title;
            }
            AddDB();
            PriceIsChanged();
        }
        /// <summary>
        /// Update price shown of item changed
        /// </summary>
        private void PriceIsChanged()
        {
            MessagingCenter.Subscribe<ItemDB>(this, "updatePrice", (item) =>
            {
                for(int x = 0; x < Info.Count; x++)
                {
                    if(Info[x].ID == item.ID)
                    {
                        Info[x] = item;

                    }
                }

                
                
            });
        }

        /// <summary>
        /// create popup to edit price of an item. Item based on ID
        /// </summary>
        /// <param name="sender">Id of item</param>
        public async void EditPrice(object sender)
        {
            ItemDB item = await App.Database.GetItemFromID(int.Parse(sender.ToString()));
            await Navigation.PushPopupAsync(new ChangePrice(item));

        }
        /// <summary>
        /// Dellete all items
        /// </summary>
        /// <returns>null</returns>
        public async Task DeleteEverything()
        {
            await App.Database.DeleteAll();
            await AddDB();
        }
        /// <summary>
        /// Delete a single item from list and db
        /// </summary>
        /// <param name="sender">Item ID</param>
        public async void DeleteItem(object sender)
        {
            foreach (ItemDB x in Info)
            {
                if (x.ID == (int)sender)
                {
                    await App.Database.DeleteItem(x);
                }
            }

            await AddDB();
        }
        /// <summary>
        /// set content to visible or invisible
        /// </summary>
        private void HideFormFromView()
        {
            if (SetVisable)
            {
                SetVisable = false;
            }
            else
            {
                SetVisable = true;
            }
        }
        /// <summary>
        /// add list to db
        /// </summary>
        /// <returns></returns>
        private async Task AddDB()
        {
            list = new ObservableCollection<ItemDB>();
            List<ItemDB> a = await App.Database.GetItemsAsync(categoryTitle);
            Debug.Write("add db");
            foreach (ItemDB temp in a)
            {
                Info.Add(temp);
            }
            OnPropertyChanged("Info");
        }

        /// <summary>
        /// Add a single item to database and empty strings from form
        /// 
        /// </summary>
        async void AddItem()
        {
            //StringFormat = '{0:c}
            string addedCategory = AddCategory;
            if(addedCategory == null)
            {
                addedCategory = PageCategory;
            }
            
            float maxPrice = FormatPrice(AddMaxPrice);
            if(maxPrice == null)
            {
                maxPrice = FormatPrice(AddPrice);
            }
            ItemDB item = new ItemDB
            {
                Name = AddName,
                Category = addedCategory,
                MinPrice = FormatPrice(AddPrice),
                MaxPrice = maxPrice,
                ItemWeightType = AddItemWeightType

            };
            AddName = string.Empty;
            AddCategory = string.Empty;
            AddPrice = string.Empty;
            AddItemWeightType = string.Empty;
            AddMaxPrice = string.Empty;
            AddItemToDB(item);
        }
        /// <summary>
        /// add an item to the database
        /// </summary>
        /// <param name="item">ItemDB object added</param>
        /// <returns></returns>
        async Task AddItemToDB(ItemDB item)
        {

        
            bool containsItem = false;
            bool containsCategory = false;
           
            foreach(ItemDB temp in Info)
            {
                //name and cat exist and are the same
                if(temp.Name.Equals(item.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    containsItem = true;
                }
                //category 
                else if (temp.Category.Equals(item.Category, StringComparison.InvariantCultureIgnoreCase))
                {
                    containsCategory = true;
                }             
            }
            Console.WriteLine(containsCategory);
            //adds item to db and list
            if (!containsItem)
            {
                await App.Database.SavePersonAsync(item);
                Info.Add(item);
                //add category to list
                if (!containsCategory)

                {
                    MessagingCenter.Send<CategoryModel>(new CategoryModel
                    {
                        Title = item.Category,
                        TargetType = typeof(ItemList)
                    }, "update");
                }
            }
            
            



        }
    


    }
}

