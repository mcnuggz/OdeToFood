﻿using OdeToFood.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        void Add(Restaurant newRestaurant);
        int Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public void Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
            
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.ToList();
        }
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            _resaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Tersiguel's" },
                new Restaurant { Id = 2, Name = "LJ's and the Kat" },
                new Restaurant { Id = 3, Name = "King's Contrivance" }
            };
        }

        public Restaurant Get(int id)
        {
           return  _resaurants.FirstOrDefault(r => r.Id == id);
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _resaurants;
        }

        public void Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _resaurants.Max(r => r.Id) + 1;
            _resaurants.Add(newRestaurant);
        }

        public int Commit()
        {
            return 0;
        }

        static List<Restaurant> _resaurants;
    }
}
