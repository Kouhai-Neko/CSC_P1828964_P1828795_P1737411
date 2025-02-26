﻿using Task2V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Task2V2.Controllers
{
    public class ProductsV2Controller : ApiController
    {[EnableCors(origins: "http://localhost:49345,http://csc123client.azurewebsites.net", headers: "*", methods: "*")]
        public class ProductsController : ApiController
        {
            static readonly IProductRepository repository = new ProductRepository();
            //Version 2
            // LIST ALL PRODUCTS //
            [HttpGet]
            [Route("api/v2/products")]
            public IEnumerable<Product> GetAllProductsFromRepository()
            {
                return repository.GetAll();

            }
            //Route constraints let you restrict how the parameters in the route template are matched. 
            //The general syntax is "{parameter:constraint}".
            //Constraints on URL parameters

            //We can even restrict the template placeholder to the type of parameter it can have. 
            //For example, we can restrict that the request will be only served if the id is greater than 2.
            //Otherwise the request will not work. For this, we will apply multiple constraints in the same request:


            //Type of the parameter id must be an integer.
            //id should be greater than 2.
            //http://localhost:9000/api2/products/1 404 error code
            // GET SPECIFIC PRODUCT FROM ID //
            [HttpGet]
            [Route("api/v2/products/{id:int:min(2)}", Name = "getProductById")]

            public Product retrieveProductfromRepository(int id)
            {
                Product item = repository.Get(id);
                if (item == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                return item;
            }

            // GET PRODUCT BY CATEGORY //
            [HttpGet]
            [Route("api/v2/products", Name = "getProductByCategory")]
            //http://localhost:9000/api2/products?category=
            //http://localhost:9000/api2/products?category=Groceries

            public IEnumerable<Product> GetProductsByCategory(string category)
            {
                return repository.GetAll().Where(
                    p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
            }


            //Response code: By default, the Web API framework sets the response status code to 200 (OK). 
            //But according to the HTTP/1.1 protocol, when a POST request results in the creation of a resource, the server should reply with status 201 (Created).
            //Location: When the server creates a resource, it should include the URI of the new resource in the Location header of the response.
            // CREATE NEW PRODUCT //
            [HttpPost]
            [Route("api/v2/products")]
            public HttpResponseMessage PostProduct(Product item)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

                // Generate a link to the new product and set the Location header in the response.

                string uri = Url.Link("getProductById", new { id = item.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }


            // UPDATE PRODUCT //
            [HttpPut]
            [Route("api/v2/products/{id:int}")]
            public void PutProduct(int id, Product product)
            {
                product.Id = id;
                if (!repository.Update(product))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }

            // DELETE PRODUCT //
            [HttpDelete]
            [Route("api/v2/products/{id:int}")]
            public void DeleteProduct(int id)
            {
                repository.Remove(id);
            }
        }

    }

}