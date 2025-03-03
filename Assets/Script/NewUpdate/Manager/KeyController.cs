using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyController : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private bool isPlayerInRange;//Kiem tra nguoi choi co trong khu vuc nhat chia khoa khong
    [SerializeField] private float timer;
    private string word;

    private GameObject player;
    //Sua thanh thoi gian
    public GameObject timebar;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        timer = 5f;
        boxCol = GetComponent<BoxCollider2D>();
        word = gameObject.name.ToUpper();
    }

    private void Update()
    {
        if (gameObject != null && timebar != null)
        {
            if (Input.GetKey(KeyCode.Space) && isPlayerInRange)
            {
                timebar.SetActive(true);
                timer -= Time.deltaTime;
                Debug.Log(timer);
                if(timer <= 0)
                {
                    CheckWords();
                }
                
            }
            else
            {
                timebar.SetActive(false);
            }
        }
    }
    //kiem tra xem vua thu thap duoc chu nao
     private void CheckWords()
    {
        if (!GameManager.instance.words.Contains(word))
        {
            GameManager.instance.words.Add(word);
            if (GameManager.instance.words.Contains(word))
            {
                LetterUIController.instance.DisplayLetter(word);
            }
        }
        player.GetComponent<Stamina>().AddStamina(1f);
        Destroy(gameObject, 0.5f);
        Destroy(timebar);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            isPlayerInRange = false;
        }
    }
}

