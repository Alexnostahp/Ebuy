using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ebuy.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int Amount { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public string UserId { get; set; }


    }
}