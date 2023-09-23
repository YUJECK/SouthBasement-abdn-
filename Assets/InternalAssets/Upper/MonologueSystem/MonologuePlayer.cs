using SouthBasement.Interactions;
using UnityEngine;
using Zenject;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologuePlayer : InteractiveGameObject
    {
        [SerializeField] private Monologue monologue;
        private MonologueManager _monologueManager;

        [Inject]
        private void Construct(MonologueManager monologueManager)
        {
            _monologueManager = monologueManager;
        }
        
        public override void OnInteractionInteracted()
        {
            _monologueManager.MoveNext();
        }

        public override void OnInteractionDetected()
        {
            _monologueManager.Start(monologue);
        }
    }
}