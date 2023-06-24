using System;
using System.Collections.Generic;

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
        
        public TComponent GetCharacterComponent<TComponent>()
            where TComponent : class
        {
            if (Components.TryGetValue(typeof(TComponent), out var component))
                return component as TComponent;

            return null;
        }

        public void AddComponent<TComponent>(ICharacterComponent component, bool replace)
            where TComponent : class
        {
            if (Components.ContainsKey(typeof(TComponent)) && replace)
            {
                Components[typeof(TComponent)] = component;
                return;
            }
            
            Components.Add(typeof(TComponent), component);
        }
        
        public void RemoveComponent<TComponent>()
            where TComponent : class
        {
            if (Components.ContainsKey(typeof(TComponent)))
                Components.Remove(typeof(TComponent));
        }
    }
}