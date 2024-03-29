﻿using SouthBasement.Helpers;
using SouthBasement.TraderItemDescriptionHUD;
using UnityEngine;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class LocationHUDInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Transform startPoint;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LocationHUDInstaller>().FromInstance(this).AsSingle();
            
            Container.InstantiatePrefab(GetDialogueWindowPrefab(), startPoint.position, 
                startPoint.rotation, null);

            var traderHUD = Container.InstantiatePrefabForComponent<TraderHUD>(GetTraderHUDPrefab(), startPoint.position, 
                startPoint.rotation, null);

            var itemInfoHUD = Container.InstantiatePrefabForComponent<ItemInfoHUD>(GetItemInfoPrefab(),
                startPoint.position, startPoint.rotation, null);
            
            Container.InstantiatePrefab(GetPauseMenuPrefab(),
                startPoint.position, startPoint.rotation, null);

            Container
                .Bind<TraderHUD>()
                .FromInstance(traderHUD)
                .AsSingle();
            
            Container
                .Bind<ItemInfoHUD>()
                .FromInstance(itemInfoHUD)
                .AsSingle();
        }

        private GameObject GetHUDPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.HUD);
        private GameObject GetPauseMenuPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.PauseMenu);
        private GameObject GetItemInfoPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.ItemInfoHUD);
        private GameObject GetDialogueWindowPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.MonologueWindow);
        private GameObject GetTraderHUDPrefab() => Resources.Load<GameObject>(ResourcesPathHelper.TraderHUD);
        public void Initialize() => Container.InstantiatePrefab(GetHUDPrefab());
    }
}