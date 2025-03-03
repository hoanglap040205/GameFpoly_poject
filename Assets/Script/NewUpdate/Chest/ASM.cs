using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASM : MonoBehaviour
{
    [SerializeField] private float staminaCollection;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            collision.gameObject.GetComponent<Stamina>().AddStamina(staminaCollection);
            Destroy(gameObject);
        }
    }
}
