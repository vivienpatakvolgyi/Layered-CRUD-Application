using D2P0JX_SG1_21_22_2.Logic.Interfaces;
using D2P0JX_SG1_21_22_2.Logic.Models;
using D2P0JX_SG1_21_22_2.Models.Entities;
using D2P0JX_SG1_21_22_2.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace D2P0JX_SG1_21_22_2.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        readonly IRestaurantLogic restaurantLogic;


        public RestaurantController(IRestaurantLogic restaurantLogic)
        {
            this.restaurantLogic = restaurantLogic;

        }


        // GET: api/Restaurant/GetAll
        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<Restaurant> Get()
        {
            return restaurantLogic.ReadAll();
        }

        // GET api/Restaurant/5
        [HttpGet("{id}")]
        public Restaurant Get(int id)
        {
            return restaurantLogic.Read(id);
        }

        // POST api/Restaurant/Create
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Post(Restaurant restaurant)
        {
            var result = new ApiResult(true);

            try
            {
                restaurantLogic.Create(restaurant);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // PUT api/Restaurant/Update
        [HttpPut]
        [ActionName("Update")]
        public ApiResult Put(Restaurant restaurant)
        {
            var result = new ApiResult(true);

            try
            {
                restaurantLogic.Update(restaurant);
            }
            catch (Exception)
            {
                result.IsSuccess = false;
            }

            return result;
        }

        // DELETE api/Restaurant/5
        [HttpDelete("{id}")]
        public ApiResult Delete(int id)
        {
            var result = new ApiResult(true);

            try
            {
                restaurantLogic.Delete(id);
            }
            catch (Exception)
            {
                
                result.IsSuccess = false;
            }

            return result;
        }
        // GET: api/Restaurant/GetRestaurantAverages
        [HttpGet]
        public IEnumerable<AverageModel> GetRestaurantAverages()
        {
            return restaurantLogic.GetRestaurantAverages();
        }

        // GET: api/Restaurant/GetMostPopularRestaurants
        [HttpGet]
        public IEnumerable<MostPopularRestaurantModel> GetMostPopularRestaurant()
        {
            return restaurantLogic.GetMostPopularRestaurant();
        }
        // GET: api/Restaurant/GetIncomes
        [HttpGet]
        public IEnumerable<IncomeModel> GetIncomes()
        {
            return restaurantLogic.GetIncomes();
        }

    }
}
