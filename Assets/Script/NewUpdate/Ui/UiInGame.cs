using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiInGame : MonoBehaviour
{

    public GameObject[] panelIngame;
    public GameObject pauseScreen;
    private bool isOpened;


    public float timeRemaining;
    private bool isTimeRemain;
    public TextMeshProUGUI timeText;

   // public static UiInGame instance;

    

    private void Start()
    {
        /*if(instance == null)
        {
            instance  = this;
        }*/
        isTimeRemain = true;
        timeRemaining = 180f;
        isOpened = false;
        for(int i = 0; i < panelIngame.Length; i++)
        {
            if (panelIngame[i].activeInHierarchy)
            {
                panelIngame[i].SetActive(isOpened);
            }
        }
    }

    private void Update()
    {
        DisPlayTimeRemain();
        //CheckTimeRemain();
    }
    public void OnClickPauseScreen()
    {
        isOpened = !isOpened;
        float timeScaler = isOpened ? 0 : 1;
        Time.timeScale = timeScaler;
        pauseScreen.SetActive(isOpened);
    }

    public void PlayAgain()
    {
        string levelPlayAgain = "Map" + PlayerPrefs.GetInt("LevelCurrent");
        SceneManager.LoadScene(levelPlayAgain);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Levels");
        SoundManager.instance.StopSound();
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("LevelCurrent", PlayerPrefs.GetInt("LevelCurrent") + 1);

        string nextLevel = "Map" + PlayerPrefs.GetInt("LevelCurrent");
        Debug.Log(nextLevel);
        SceneManager.LoadScene(nextLevel);

    }
    //Check time
    //Check dieu kien kiem tra thoi gian
    private bool CheckTimeRemain()
    {
        if (isTimeRemain)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                return true;
            }
            else
            {
                timeRemaining = 0f;
                isTimeRemain = false;
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    //hien thi thoi gian
    private void DisPlayTimeRemain()
    {
        
        if (CheckTimeRemain())
        {
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timeText.text = "Thoi gian da het";
            StartCoroutine(GameOver());
        }

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.gameOverEvent.Invoke();
    }


}
