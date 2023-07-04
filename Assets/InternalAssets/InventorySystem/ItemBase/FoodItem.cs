﻿using System;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public abstract class FoodItem : Item
    {
        public override Type GetItemType()
        {
            return typeof(FoodItem);
        }

        public abstract void Eat();
    }
}