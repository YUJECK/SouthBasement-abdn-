using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    public sealed class ComponentContainer
    {
        private readonly Dictionary<Type, ICharacterComponent> _components = new();

        public void StartAll()
        {
            foreach (var componentPair in _components)
                componentPair.Value.OnStart();
        }

        public void UpdateALl()
        {
            foreach (var componentPair in _components)
                componentPair.Value.OnUpdate();
        }
        
        public void DisposeAll()
        {
            foreach (var componentPair in _components)
                componentPair.Value.Dispose();
        }

        public bool Contains<TComponent>() 
            where TComponent : class
            => _components.ContainsKey(typeof(TComponent));
        
        public TComponent Get<TComponent>()
            where TComponent : class
        {
            if (_components.TryGetValue(typeof(TComponent), out var component))
                return component as TComponent;

            return null;
        }

        public bool TryGet<TComponent>(out TComponent component)
            where TComponent : class
        {
            if (_components.TryGetValue(typeof(TComponent), out var result))
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
            if (_components.ContainsKey(typeof(TComponent)))
            {
                Debug.LogWarning("Components Container already contains component of type " + typeof(TComponent).Name);
                return this;
            }
            
            _components.Add(typeof(TComponent), component);
            component.OnStart();

            return this;
        }
        
        public ComponentContainer Replace<TComponent>(ICharacterComponent newComponent)
            where TComponent : class
        {
            if (_components.ContainsKey(typeof(TComponent)))
            {
                _components[typeof(TComponent)] = newComponent;
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
            if (_components.ContainsKey(typeof(TComponent)))
            {
                _components[typeof(TComponent)].Dispose();
                _components.Remove(typeof(TComponent));
            }
            
            return this;
        }
    }
}