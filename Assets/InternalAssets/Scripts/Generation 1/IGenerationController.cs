using System;

public interface IGenerationController
{
    event Action OnGenerationStarted;
    event Action OnGenerationEnded;

    void StartGeneration();
    void Regenerate();
}