using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class LetterUIController : MonoBehaviour
{
    [SerializeField] GameObject panelLetter;
    [SerializeField] Animator anim;
    [SerializeField] private float timer;
    [SerializeField] private Image[] letters;
    public static LetterUIController instance;
    private Color lerpColor;
    public bool isComplete;
    public int countBee = 0;
    private void Start()
    {
        isComplete = false;
        lerpColor = Color.black;
        for(int i = 0; i < letters.Length; i++)
        {
            letters[i].color = lerpColor;
        }
        if (instance == null)
        {
            instance = this;
        }
        StartCoroutine(ClosedPanel());
    }
    //Opened Panel
    IEnumerator OpenedPanel()
    {
        yield return new WaitForSeconds(1f);
        panelLetter.SetActive(true);
        anim.SetTrigger("Opened");

        //Kiem tra dieu kien neu du chu thi mo collectedAll 
        if (GameManager.instance.IsGameWin())
        {
            Debug.Log("WinGame");
            StartCoroutine(CollectedAll());
        }
        else
        {
            StartCoroutine(ClosedPanel());
        }
    }
    //Closed panel
    IEnumerator ClosedPanel()
    {
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Closed");
        yield return new WaitForSeconds(1.3f);
        panelLetter.SetActive(false);
    }
    //Mo khi thu thap du chu
    IEnumerator CollectedAll()
    {
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("CollectedAll");
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(ClosedPanel());
        isComplete = true;
    }
    //Khi thu nhap mot word thi anim opened va chu do se duoc to mau
    public void DisplayLetter(string wordCollected)
    {
        for(int i = 0; i < letters.Length; i++)
        {
            if (letters[i].name  == wordCollected)
            {
                letters[i].color = Color.white;
                StartCoroutine(OpenedPanel());
                break;
            }
        }
    }

    






}
