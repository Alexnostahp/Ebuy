using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ebuy.Models
{
    public class BoughtItem
    {
        public int BoughtItemId { get; set; }

        public string UserId { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }


    }
}