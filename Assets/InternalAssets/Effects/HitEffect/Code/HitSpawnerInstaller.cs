using UnityEngine;
using Zenject;

namespace SouthBasement.Effects
{
    public sealed class HitSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private HitEffect hitEffectPrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<HitEffectSpawner>()
                .FromInstance(new HitEffectSpawner(hitEffectPrefab))
                .AsSingle();
        }
    }
}