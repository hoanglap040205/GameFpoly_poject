using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaBar : MonoBehaviour
{
    public Image currentStamina;
    public Image staminaBar;
    public Stamina stamina;
    
    
    
    private void Awake()
    {
        currentStamina.fillAmount = stamina.currentStamina / stamina.startStamina;

    }
    private void Update()
    {
        currentStamina.fillAmount = stamina.currentStamina / stamina.startStamina;
    }
}
