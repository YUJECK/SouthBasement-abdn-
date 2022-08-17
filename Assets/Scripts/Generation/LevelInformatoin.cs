using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformatoin : MonoBehaviour
{
    [SerializeField] private string levelName = "FirstLevelBasement"; //Название уровня
    [SerializeField] private string locationName = "Basement"; //Название локации
    [SerializeField] private Sprite levelIcon; //Иконка локации
    [SerializeField] private string[] music; //Музыка которая может играть на этом уровне

    //Геттеры для имени уровня и локации
    public string LevelName => levelName;
    public string LocationName => locationName;

    //Ставим играть музыку
    private void Start() { if(music.Length > 0) ManagerList.AudioManager.SetToMain(music[Random.Range(0, music.Length)]); }
}