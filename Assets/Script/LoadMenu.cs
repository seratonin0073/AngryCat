using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    public int level = 1;

    private void Start()
    {
        LoadStars();
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    private void LoadStars()
    {
        if(gameObject.CompareTag("LevelButton"))
        {
            int loadStars = PlayerPrefs.GetInt($"Level{level}");
            transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "stars: " + loadStars;
            
            int currentOpenLevel = PlayerPrefs.GetInt("OpenLevel");

            if (currentOpenLevel == 0)
            {
                currentOpenLevel = 1;
            }

            int allLevels = SceneManager.sceneCountInBuildSettings;

            if (currentOpenLevel >= level && level < allLevels) GetComponent<Button>().interactable = true;
            else GetComponent<Button>().interactable = false;
        }
    }
}
