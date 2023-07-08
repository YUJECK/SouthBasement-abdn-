using UnityEngine.SceneManagement;
using Zenject;

namespace SouthBasement
{
    public sealed class RunStarter
    {
        private readonly RunDatabase _runDatabase;

        [Inject]
        public RunStarter(RunDatabase runDatabase)
        {
            _runDatabase = runDatabase;
        }
        
        public void StartRun()
        {
            _runDatabase.Reset();

            SceneManager.LoadScene("FirstLevel");
        }
    }
}