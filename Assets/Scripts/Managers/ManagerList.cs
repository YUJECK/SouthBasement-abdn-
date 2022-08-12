using Generation;
using UnityEngine;

public class ManagerList : MonoBehaviour
{
    private static GameManager gameManager;
    private static SkinManager skinManager;
    private static AudioManager audioManager;
    private static EffectsInfo effectsInfo;
    private static GenerationManager generationManager;
    private static Grid grid;

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
    public static Grid Grid
    {
        get
        {
            if (grid == null) grid = FindObjectOfType<Grid>();
            return grid;
        }
        private set => grid = value;
    }

    private void Awake()
    {
        Debug.Log("[Info]: " + GameManager.name + " has been setted");
        Debug.Log("[Info]: " + SkinManager.name + " has been setted");
        Debug.Log("[Info]: " + AudioManager.name + " has been setted");
        Debug.Log("[Info]: " + EffectsInfo.name + " has been setted");
        Debug.Log("[Info]: " + GenerationManager.name + " has been setted");
        Debug.Log("[Info]: " + Grid.name + " has been setted");
    }
}