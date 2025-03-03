using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUiManager : MonoBehaviour
{
    [SerializeField] private GameObject selectorLevelsPanel;
    [SerializeField] private GameObject topPlayerPanel;
    private bool isOpened;
    private void Awake()
    {
        isOpened = false;
        topPlayerPanel.SetActive(false);
        selectorLevelsPanel.SetActive(true);
    }

    public void OnClickTopPlayer()
    {
        isOpened = !isOpened;
        topPlayerPanel.SetActive(isOpened);
    }
}
