using AmazonSearch.AmazonWebService;
using System;
using System.Collections.Generic;
using System.Web;
using AmazonSearch.AmazonWebService;
using AmazonServiceSignature;
using System.ServiceModel;
using System.Threading;

namespace AmazonSearch.Models
{
    public class ProductContainer
    {
        private List<Product> products;
        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        private int resultCount = 0;
        public int ResultCount
        {
            get { return resultCount; }
            set { resultCount = value; }
        }

        private int pageCount = 0;
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }

        protected static ProductContainer _getProducts(String search, int pageNum)
        {
            List<Product> products = new List<Product>();
            int pageCount = 0;
            int totalResults = 0;

            //prepare an ItemSearch request
            ItemSearchRequest request = new ItemSearchRequest();
            request.SearchIndex = "All";
            request.Keywords = search;
            request.ResponseGroup = new string[] { "ItemAttributes", "Small", "Images" };
            request.ItemPage = pageNum.ToString();

            ItemSearchResponse res = AmazonService.search(request);
            if (res.Items[0].Item != null)
            {
                foreach (var item in res.Items[0].Item)
                {
                    if (item == null) continue;
                    string url = item.DetailPageURL;

                    if (url.EndsWith("null"))
                        url = url.Remove(url.Length - 4);

                    int price = -1;
                    string formattedPrice = "";
                    if (item.ItemAttributes.ListPrice != null)
                    {
                        price = Convert.ToInt32(item.ItemAttributes.ListPrice.Amount);
                        formattedPrice = item.ItemAttributes.ListPrice.FormattedPrice;
                    }

                    Product product = new Product
                    {
                        Title = item.ItemAttributes.Title,
                        Link = url,
                        Image = item.MediumImage,
                        Price = price,
                        FormattedPrice = formattedPrice
                    };

                    products.Add(product);

                }
            }

            if (products.Count > 0)
            {
                totalResults = Convert.ToInt32(res.Items[0].TotalResults);
                if (totalResults > 0)
                {
                    if (totalResults <= Globals.ITEMS_PER_PAGE)
                        pageCount = 1;
                    else
                    {
                        pageCount = totalResults / Globals.ITEMS_PER_PAGE;
                        if (totalResults % Globals.ITEMS_PER_PAGE != 0)
                            pageCount++;
                    }
                }
            }

            return new ProductContainer
            {
                products = products,
                pageCount = pageCount,
                resultCount = totalResults
            };
        }

        public static ProductContainer getProducts(String search, int pageNum)
        {
            List<Product> products = new List<Product>();
            int pageCount = 0;
            int totalResults = 0;

            int start = (pageNum - 1) * Globals.ITEMS_PER_PAGE;       
            int firstElem = start % Globals.AMAZON_ITEMS_PER_PAGE; 
            int lastElem = firstElem + Globals.ITEMS_PER_PAGE; 
            int currentPage = pageNum;

            ProductContainer tmpProducts = new ProductContainer();
            for (int i = firstElem; i < lastElem; i++)
            {
                if (i == firstElem || i == Globals.AMAZON_ITEMS_PER_PAGE)
                {
                    tmpProducts = _getProducts(search, currentPage);
                    pageCount = tmpProducts.pageCount;
                    totalResults = tmpProducts.resultCount;
                    currentPage++;
                }

                if (i % Globals.AMAZON_ITEMS_PER_PAGE >= tmpProducts.products.Count)
                    break;

                products.Add(tmpProducts.products[i % Globals.AMAZON_ITEMS_PER_PAGE]);
            }

            return new ProductContainer
            {
                products = products,
                pageCount = pageCount,
                resultCount = totalResults
            };
        }
    }
}