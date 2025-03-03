using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public static UnityEvent gameOverEvent = new UnityEvent();
    public static UnityEvent gameWinEvent = new UnityEvent();

    public static GameManager instance;
    private DataUserManager data;
    public GameObject gameWin;
    public GameObject gameOver;
    public List<string> words = new List<string>();

    
    private void Awake()
    {
        gameOverEvent.AddListener(GameOver);
        gameWinEvent.AddListener(GameWin);
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
        StartCoroutine(ReadyGame());
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        
    }

    //Win
    private void GameWin()
    {
        Debug.Log("Ban da chien thang");
        UnLockLevel();
        gameWin.SetActive(true);
        Time.timeScale = 0f;
    }
    //Kiem tra xem da thu thap du chu chua
     public bool IsGameWin()
    {
        if(words.Count == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //mo khoa level moi
    private void UnLockLevel()
    {
       if(PlayerPrefs.GetInt("LevelCurrent") >= PlayerPrefs.GetInt("LevelMaxCurrent"))
        {
            PlayerPrefs.SetInt("LevelMaxCurrent", PlayerPrefs.GetInt("LevelCurrent") + 1);
            PlayerPrefs.Save();
            Debug.Log("LevelMaxCurrent" + PlayerPrefs.GetInt("LevelMaxCurrent"));
        }
       
    }
    
    //Chuan bi vao game
    IEnumerator ReadyGame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;
    }

    
}
