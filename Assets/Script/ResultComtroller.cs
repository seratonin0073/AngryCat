using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultComtroller : MonoBehaviour
{
    [SerializeField] private int loadStars;///кількість зірок
    [SerializeField] private float timeToThreeStart;
    [SerializeField] private float timeCofficient;

    [SerializeField] private Text gameTimerText;
    [SerializeField] private Text allTimeText;//текст в якому показується час за який пройдено рівень
    [SerializeField] private Text starText;

    [SerializeField] private GameObject gameInterface;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject[] interfaceButton;

    private float currentLevelTime;

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
}
