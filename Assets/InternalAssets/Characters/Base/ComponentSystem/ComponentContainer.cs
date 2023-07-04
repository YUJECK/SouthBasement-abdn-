﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    public sealed class ComponentContainer
    {
        protected Dictionary<Type, ICharacterComponent> Components = new();

        public void StartAll()
        {
            foreach (var componentPair in Components)
                componentPair.Value.OnStart();
        }

        public void UpdateALl()
        {
            foreach (var componentPair in Components)
                componentPair.Value.OnUpdate();
        }
        
        public void DisposeAll()
        {
            foreach (var componentPair in Components)
                componentPair.Value.Dispose();
        }

        public bool Contains<TComponent>() 
            where TComponent : class
            => Components.ContainsKey(typeof(TComponent));
        
        public TComponent Get<TComponent>()
            where TComponent : class
        {
            if (Components.TryGetValue(typeof(TComponent), out var component))
                return component as TComponent;

            return null;
        }

        public bool TryGet<TComponent>(out TComponent component)
            where TComponent : class
        {
            if (Components.TryGetValue(typeof(TComponent), out var result))
            {
                component = result as TComponent;
                return true;
            }

            component = null;
            return false;
        }
        
        public ComponentContainer Add<TComponent>(ICharacterComponent component)
            where TComponent : class
        {
            if (Components.ContainsKey(typeof(TComponent)))
            {
                Debug.LogWarning("Components Container already contains component of type " + typeof(TComponent).Name);
                return this;
            }
            
            Components.Add(typeof(TComponent), component);
            component.OnStart();

            return this;
        }
        
        public ComponentContainer Replace<TComponent>(ICharacterComponent newComponent)
            where TComponent : class
        {
            if (Components.ContainsKey(typeof(TComponent)))
            {
                Components[typeof(TComponent)] = newComponent;
                newComponent.OnStart();
            }
            else
            {
                Debug.LogWarning("There is no component of type " + typeof(TComponent).Name);
            }

            return this;
        }

        public ComponentContainer Remove<TComponent>()
            where TComponent : class
        {
            if (Components.ContainsKey(typeof(TComponent)))
            {
                Components[typeof(TComponent)].Dispose();
                Components.Remove(typeof(TComponent));
            }
            
            return this;
        }
    }
}