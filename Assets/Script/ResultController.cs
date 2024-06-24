using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultController : MonoBehaviour
{
    public static ResultController Instance;

    private int loadStars;//кількість зірок
    public static int currentOpenLevel = 1;//кількість пройдених рівнів

    [SerializeField] private float timeToThreeStart;
    [SerializeField] private float timeCofficient;

    [SerializeField] private TMP_Text gameTimerText;
    [SerializeField] private TMP_Text allTimeText;//текст в якому показується час за який пройдено рівень
    [SerializeField] private TMP_Text starText;

    [SerializeField] private GameObject gameInterface;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject[] interfaceButton;

    private float currentLevelTime;

    void Start()
    {
        Instance = this;
        LoadResoult();
        resultPanel.SetActive(false);
        currentOpenLevel = PlayerPrefs.GetInt("OpenLevel");
        if(currentOpenLevel == 0)
        {
            currentOpenLevel = 1;
        }
    }
    private void Tick()
    {
        currentLevelTime += 0.1f;
        gameTimerText.text = string.Format("{0:N1} s", currentLevelTime);
    }

    public void StartTimer()
    {
        InvokeRepeating("Tick", 0f, 0.1f);
    }

    public void StopTimer()
    {
        CancelInvoke();
    }

    public void LoadResoult()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        loadStars = PlayerPrefs.GetInt($"Level{levelIndex}");
    }

    public void SaveResoult()
    {
        int stars = 0;
        if(currentLevelTime <= timeToThreeStart)
        {
            stars = 3;
        }
        else if(currentLevelTime > timeToThreeStart && 
            currentLevelTime < timeToThreeStart * timeCofficient) 
        {
            stars = 2; 
        }
        else 
        {
            stars = 1;
        }

        resultPanel.SetActive(true);
        gameInterface.SetActive(false);
        allTimeText.text = string.Format("time: {0:N1} s", currentLevelTime);

        
        currentOpenLevel++;
        PlayerPrefs.SetInt("OpenLevel", currentOpenLevel);

        if(stars >= loadStars)
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt($"Level{levelIndex}", stars);
            starText.text = $"stars: {stars}";
            interfaceButton[0].SetActive(false);
            interfaceButton[1].SetActive(true);
        }
        else
        {
            starText.text = $"stars: {stars}";
            interfaceButton[0].SetActive(true);
            interfaceButton[1].SetActive(false);
        }
    }

    public void Restart()
    {
        int levelIndedx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndedx);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        int allLevels = SceneManager.sceneCountInBuildSettings;

        if(levelIndex < allLevels)  SceneManager.LoadScene(levelIndex + 1);
    }

    public void AddTime(int time)
    {
        currentLevelTime += time;
    }
    public void RemoveTime(int time)
    {
        currentLevelTime -= time;
        if (currentLevelTime < 0) currentLevelTime = 0;
    }

    
}
