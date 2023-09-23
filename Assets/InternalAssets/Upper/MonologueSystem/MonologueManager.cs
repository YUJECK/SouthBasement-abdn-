using System;

namespace SouthBasement.MonologueSystem
{
    public sealed class MonologueManager
    {
        public event Action<Monologue> OnStarted;
        public event Action<Monologue> OnStopped;
        public event Action<string> OnNext;

        public Monologue Current { get; private set; }
        public int Stage { get; private set; }
        
        public void Start(Monologue monologue)
        {
            OnStarted?.Invoke(monologue);
            Current = monologue;
        }

        public void Stop(Monologue monologue)
        {
            OnStopped?.Invoke(monologue);  
            Current = null;
        }

        public void MoveNext()
        {
            if (Current != null)
                Stage++;
            if(Current.phrases.Length <= Stage)
                Stop(Current);
            
            OnNext?.Invoke(Current.phrases[Stage]);
        }
    }
}