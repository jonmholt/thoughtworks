using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thoughtworks
{
    public class ReceiptFormatter
    {
        static public string getReceipt(List<IItem> list)
        {
            double tax = 0;
            double total = 0;
            string receipt = "";
            foreach (IItem item in list)
            {
                receipt += String.Format("1 {0}: {1:c}\n", item.Description, item.ShelfPrice);
                tax += item.Tax;
                total += item.ShelfPrice;
            }
            receipt += String.Format("Sales Taxes: {0:c}\n",tax);
            receipt += String.Format("Total: {0:c}\n", total);
            return receipt;
        }
    }
}
