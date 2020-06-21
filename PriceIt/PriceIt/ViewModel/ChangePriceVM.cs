using PriceIt.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

namespace PriceIt.ViewModel
{
    public class ChangePriceVM : BaseVM
    {
        /// <summary>
        /// update price to new price
        /// </summary>
        public ICommand OnUpdatePrice { get; private set; }
        private int id;

        /// <summary>
        /// sets command to update price sets ID and item to update
        /// </summary>
        /// <param name="sender">Item that will be changed</param>
        public ChangePriceVM(ItemDB sender)
        {
            Item = sender;
            id = sender.ID;
            OnUpdatePrice = new Command(async() => await UpdatePrice());
           
        }

        /// <summary>
        /// updates price of an item based either min or max based on the price submited
        /// </summary>
        /// <returns></returns>
        private async Task UpdatePrice()
        {
            ItemDB temp = await App.Database.GetItemFromID(id);
            float price = FormatPrice(ChangePrice);
            
            if(price < temp.MinPrice && price != 0)
            {
                temp.MinPrice = price;
            }
            else if(price > temp.MaxPrice && price != 0)
            {
                temp.MaxPrice = price;
            }
            MessagingCenter.Send<ItemDB>(temp, "updatePrice");
            await App.Database.UpdatePrice(temp);
            await PopupNavigation.Instance.PopAsync();

        }
        
        /// <summary>
        /// input to change the price
        /// </summary>
        public string ChangePrice
        {
            get; set;
        }

        /// <summary>
        /// display information about the item
        /// </summary>
        public ItemDB Item
        {
            get; 
            private set;
        }
    }
}
