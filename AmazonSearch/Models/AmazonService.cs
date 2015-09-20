using System;
using System.Web;
using System.Collections.Generic;
using System.ServiceModel;
using AmazonSearch.AmazonWebService;
using AmazonServiceSignature;

namespace AmazonSearch.Models
{
    public class AmazonService
    {
        //returns response from Amazon search request
        public static ItemSearchResponse search(ItemSearchRequest request)
        {
            // create a WCF Amazon ECS client
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.MaxReceivedMessageSize = int.MaxValue;
            AWSECommerceServicePortTypeClient client = new AWSECommerceServicePortTypeClient(binding,
                    new EndpointAddress("https://webservices.amazon.com/onca/soap?Service=AWSECommerceService"));

            // add authentication to the ECS client
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(Globals.ACCESS_KEY_ID, Globals.SECRET_KEY));

            ItemSearch itemSearch = new ItemSearch();
            itemSearch.Request = new ItemSearchRequest[] { request };
            itemSearch.AWSAccessKeyId = Globals.ACCESS_KEY_ID;
            itemSearch.AssociateTag = "";

            // issue the ItemSearch request
            return client.ItemSearch(itemSearch);
        }      
    }
}