using UnityEngine;
using UnityEngine.UI;

public class LevelInformatoin : MonoBehaviour
{
    [Header("Информация")]
    [SerializeField] private string levelName = "FirstLevelBasement"; //Название уровня
    [SerializeField] private string locationName = "Basement"; //Название локации
    [SerializeField] private Sprite levelIcon; //Иконка локации
    [SerializeField] private string[] music; //Музыка которая может играть на этом уровне
    [Header("Интерфейс")]
    [SerializeField] private string outPutLevelName; //Название уровня которое будет выводится у игрока
    [SerializeField] private string outPutLocationName; //Название уровня будет выводится у игрока

    //Геттеры для имени уровня и локации
    public string LevelName => levelName;
    public string LocationName => locationName;
    public void UpdateUIInformationAboutLevel(Image levelUIIcon, Text informationUIText)
    {
        informationUIText.text =
            outPutLocationName + "\n" +
            outPutLevelName;
        levelUIIcon.sprite = levelIcon;
    }

    //Ставим играть музыку
    private void Start()  { if (music.Length > 0) ManagerList.AudioManager.SetToMain(music[Random.Range(0, music.Length)]); }
}