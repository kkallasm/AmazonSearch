using AmazonSearch.AmazonWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonSearch.Models
{
    public class Product
    {
        private String title;
        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private string formattedPrice;
        public string FormattedPrice
        {
            get { return formattedPrice; }
            set { formattedPrice = value; }
        }

        private String link;
        public String Link
        {
            get { return link; }
            set { link = value; }
        }

        private Image image;
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

    }
}