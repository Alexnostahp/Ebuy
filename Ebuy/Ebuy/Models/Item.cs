using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ebuy.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [RegularExpression("[a-zA-Zåäö/0-9 ]{1,255}", ErrorMessage = "Only characters and numbers allowed.")]
        public string ItemName { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [RegularExpression("[0-9]{0,10}", ErrorMessage = "Only numbers allowed")]
        public string Price { get; set; }

        public string Description { get; set; }

        [Required]
        [RegularExpression("[a-zA-Zåäö/0-9 ]{1,255}", ErrorMessage = "Only characters and numbers allowed.")]
        public string Location { get; set; }

        [Required]
        [RegularExpression("[0-9]{0,6}", ErrorMessage = "Only numbers allowed")]
        public int Quantity { get; set; }


        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}