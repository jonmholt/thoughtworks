using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thoughtworks
{
    public class Food:Item
    {
        public override bool hasSalesTax
        {
            get
            {
                return false;
            }
        }
    }
}
