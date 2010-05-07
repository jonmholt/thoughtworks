using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thoughtworks
{
    public class Item:IItem
    {
        private const double salesTax = 0.1;
        private const double importDuty = 0.05;
        private double _price;
        private double _tax;
        private bool _imported;
        private string _description;
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {   
                //set price
                _price = value;
                //calculate tax
                double tax = 0;
                if (this.hasSalesTax)
                {

                    tax = Math.Ceiling((_price * salesTax) * 20) / 20;

                }
                if (this.isImported)
                {
                    tax += Math.Ceiling((_price * importDuty) * 20) / 20;
                }
                //set tax (man...I hate floating point and money)
                this.Tax = Math.Round(tax,2);
            }

        }
        public double Tax
        {
            get
            {
                return _tax;

            }
            set
            {
                _tax = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;

            }
            set
            {
                _description= value;
            }
        }
        public double ShelfPrice
        {
            get
            {
                //bloody floating point
                return Math.Round(_tax + _price, 2);
            }
        }
        public bool isImported {
            get
            {
                return _imported;
            }
            set
            {
                _imported = value;
            }
        }
        public virtual bool hasSalesTax {
            get
            {
                return true;
            }
        }
    }
}
