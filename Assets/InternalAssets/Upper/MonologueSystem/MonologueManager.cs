using System;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologueManager
    {
        public event Action<Monologue> OnStarted;
        public event Action<Monologue> OnStopped;
        public event Action<string> OnSentence;

        public Monologue Current { get; private set; }
        public int Stage { get; private set; }
        
        public void Start(Monologue monologue)
        {
            Current = monologue;
            Stage = Monologue.MinimumStage;

            OnStarted?.Invoke(monologue);
            OnSentence?.Invoke(Current.GetPhrase(Stage));
        }

        public void StopCurrent()
        {
            OnStopped?.Invoke(Current);  
            Current = null;
        }

        public void MoveNext()
        {
            if (Current == null)
                return;
            
            Stage++;

            if (Current.IsCompleted(Stage))
            {
                StopCurrent();
                return;
            }
            
            OnSentence?.Invoke(Current.GetPhrase(Stage));
        }
    }
}