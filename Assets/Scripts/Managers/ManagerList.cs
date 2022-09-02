using Generation;
using UnityEngine;
using Creature.Pathfind;
public class ManagerList : MonoBehaviour
{
    private static GameManager gameManager;
    private static SkinManager skinManager;
    private static AudioManager audioManager;
    private static EffectsInfo effectsInfo;
    private static GenerationManager generationManager;
    private static Creature.Pathfind.Grid grid;

    public static GameManager GameManager
    {
        get
        {
            if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
            return gameManager;
        }
        private set => gameManager = value;
    }
    public static SkinManager SkinManager
    {
        get
        {
            if (skinManager == null) skinManager = FindObjectOfType<SkinManager>();
            return skinManager;
        }
        private set => skinManager = value;
    }
    public static AudioManager AudioManager
    {
        get
        {
            if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();
            return audioManager;
        }
        private set => audioManager = value;
    }
    public static EffectsInfo EffectsInfo
    {
        get
        {
            if (effectsInfo == null) effectsInfo = FindObjectOfType<EffectsInfo>();
            return effectsInfo;
        }
        private set => effectsInfo = value;
    }
    public static GenerationManager GenerationManager
    {
        get
        {
            if (generationManager == null) generationManager = FindObjectOfType<GenerationManager>();
            return generationManager;
        }
        private set => generationManager = value;
    }
    public static Creature.Pathfind.Grid Grid
    {
        get
        {
            if (grid == null) grid = FindObjectOfType<Creature.Pathfind.Grid>();
            return grid;
        }
        private set => grid = value;
    }
    private void OnLevelWasLoaded(int level)
    {
        generationManager = FindObjectOfType<GenerationManager>();
        grid = FindObjectOfType<Creature.Pathfind.Grid>();
        Debug.Log("[Info]: " + GenerationManager.name + " has been updated");
        Debug.Log("[Info]: " + Grid.name + " has been updated");
    }
}