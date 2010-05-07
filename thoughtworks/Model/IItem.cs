using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thoughtworks
{
    public interface IItem
    {
        double Price{
            get;
            set;
        }
        double Tax{
            get;
            set;
        }        
        bool isImported{
            get;
            set;
        }
        string Description
        {
            get;
            set;
        }
        //Read only
        double ShelfPrice
        {
            get;
        }
        bool hasSalesTax
        {
            get;
        }
    }
}
