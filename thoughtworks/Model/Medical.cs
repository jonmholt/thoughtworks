using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thoughtworks
{
    public class Medical:Item
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
