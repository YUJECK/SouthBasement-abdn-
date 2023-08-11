using SouthBasement.Characters.Base;
using UnityEngine.SceneManagement;
using Zenject;

namespace SouthBasement
{
    public sealed class RunController
    {
        private readonly RunDatabase _runDatabase;

        [Inject]
        public RunController(RunDatabase runDatabase)
        {
            _runDatabase = runDatabase;
        }
        
        public void StartRun()
        {
            _runDatabase.Reset();
            SceneManager.LoadScene("FirstLevel");
        }

        public void EndRun()
        {
            _runDatabase.Remove();
        }

        public void SwitchToNextLevel()
        {
            
        }

        public void OnCharacterSpawned(CharacterGameObject character)
        {
            _runDatabase.OnCharacterSpawned(character);
        }
    }
}