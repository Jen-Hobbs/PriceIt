using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PriceIt.ViewModel
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// invoke property changed event based on name of property
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// ensure that a string can be converted to price format and return the price
        /// </summary>
        /// <param name="priceUnformated">price in unformated string</param>
        /// <returns>price</returns>
        protected float FormatPrice(string priceUnformated)
        {
            Console.WriteLine("Formate Price");
            Console.WriteLine(priceUnformated);
            float price = 0;
            if(float.TryParse(String.Format("{0:c}", priceUnformated), out price))
            {

                Console.WriteLine(price);
                return price;    
            };
            Console.WriteLine(price);
            
                return price;
            
        }
    }
}
