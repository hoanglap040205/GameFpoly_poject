using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] buttonLevels;
    [SerializeField] DataUserManager data;
    private void Start()
    {
        ReSetLevelPlayer();
    }

    public void OpenedIDLevel(int idSelected)
    {
        string levelID = "Map" + idSelected;
        PlayerPrefs.SetInt("LevelCurrent",idSelected);
        Debug.Log("LevelSlectedCurrent" + PlayerPrefs.GetInt("LevelCurrent"));
        SceneManager.LoadScene(levelID);
    }

    private void ReSetLevelPlayer()
    {
        for (int i = 1; i < buttonLevels.Length; i++)
        {
            buttonLevels[i].interactable = false;
        }

        for (int i = 1; i < PlayerPrefs.GetInt("LevelMaxCurrent"); i++)
        {
            buttonLevels[i].interactable = true;
        }
    }

}
