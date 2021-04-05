using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Main_Menu : MonoBehaviour
{

    public string startLevel;
    public string Levels;

    public string level1Tag;

    public int playerLives;

    public int playerHealth;



    public void NewGame()
    {
        SceneManager.LoadScene(startLevel);
        PlayerPrefs.SetInt(level1Tag, 1);
    }
    /*
    public void LevelSelect()
    {

        SceneManager.LoadScene(Levels);
        PlayerPrefs.SetInt(level1Tag, 1);


    }*/

    public void QuitGame()
    {
        Application.Quit();

    }
}
