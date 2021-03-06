﻿using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class Patrimony : IPatrimony
    {
        public decimal Cash { get; set; }
        public int Count => _propertyList.Count;

        private readonly List<Property> _propertyList;

        public Patrimony(decimal cash)
        {
            Cash = cash;
            _propertyList = new List<Property>(16);
        }

        public bool Exchange(Property sellerProperty, IPatrimony buyer)
        {
            return Exchange(sellerProperty, sellerProperty.BuyPrice, buyer);
        }

        public bool Exchange(Property sellerProperty, decimal price, IPatrimony buyer)
        {
            var seller = this;

            if(!buyer.Debit(price))
                return false;
            seller.Credit(price);

            if (!seller.Debit(sellerProperty))
                return false;
            buyer.Credit(sellerProperty);

            return true;
        }

        public void Credit(decimal cash)
        {
            Cash += cash;
        }

        public bool Debit(decimal cash)
        {
            if (Cash - cash < 0)
                return false;
            Cash -= cash;
            return true;
        }

        public void Credit(Property property)
        {
            _propertyList.Add(property);
        }

        public bool Debit(Property property)
        {
            var item = 
                _propertyList.
                    FirstOrDefault(_ =>
                        _.Name == property.Name);
            return item != null && _propertyList.Remove(item);
        }
        
        public bool Owns(Square property)
        {
            var ret = 
                _propertyList.
                    FirstOrDefault(_ =>
                        _.Name == property.Name);
            return ret != null;
        }
    }
}