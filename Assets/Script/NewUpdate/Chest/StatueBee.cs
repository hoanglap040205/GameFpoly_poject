using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueBee : MonoBehaviour
{
    public BoxCollider2D boxCol;
    private bool isPlayerInRange;//Kiem tra xem player co trong khu vuc co the mo ong khong
    [SerializeField]private bool isPlayerInBee = false;//kiem tra xem player co trong ong khong
    private GameObject player;
    [SerializeField] private Transform target;
    //Tao chuc nang kiem tra xem ui hoan thanh chua,neu hoan thanh roi thi di chuyen toi tuong ong lon
    public float movespeed;
    private void Awake()
    {
        movespeed = 3f;
        boxCol = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("player");
    }
    //nguoi choi nhan f de chu vao ruong f lan nua de thoat
    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!isPlayerInBee)//nguoi choi co the vao ruong
                {
                    StartCoroutine(EnterBee());
                }
            }
        }
        else if (!isPlayerInRange && isPlayerInBee && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ExitBee());
        }

        //Khi isCompelete hoan thanh thi EnterBigBee hoat dong
        if(LetterUIController.instance.isComplete == true)
        {
            EnterBigBee();
        }

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
    IEnumerator EnterBee()
    {
        yield return new WaitForSeconds(0.3f);
        StudentController.EnterChestEvent.Invoke();
        StartCoroutine(MoveInStatuBEE());
        
        
        isPlayerInBee = !isPlayerInBee;//nguoi choi dang o trong ruong 
    }

    IEnumerator MoveInStatuBEE()
    {

        float timer = 0f;
        while (timer < 0.3f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position,3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;

        }
        
    }
    IEnumerator ExitBee()
    {
        yield return new WaitForSeconds(0.2f);
        StudentController.ExitChestEvent.Invoke();
        isPlayerInBee = !isPlayerInBee;//nguoi choi thoat ra khoi ruong
    }
    // nhap vao ong khi ui hoan thanh
    private void EnterBigBee()
    {
        boxCol.enabled = false;
        if (!isPlayerInBee)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, movespeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, target.position) == 0)
            {
                LetterUIController.instance.countBee++;
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Nguoi choi van con ben trong");
        }
    }


    
}
