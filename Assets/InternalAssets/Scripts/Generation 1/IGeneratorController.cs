using System;

public interface IGeneratorController
{
    event Action OnGenerationStarted;
    event Action OnGenerationEnded;

    void StartGeneration();
    void Regenerate();
}