using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float startStamina;
    public float currentStamina;
    private StudentController student;
    private void Awake()
    {
        currentStamina = startStamina;
        student = GetComponent<StudentController>();
    }
    
    public bool TakeStamina(float _StaminaAmount)
    {
        if (currentStamina > 0)
        {
            currentStamina = Mathf.Clamp(currentStamina - _StaminaAmount, 0, 100);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddStamina(float _staminaCollectionAmount)
    
    {
        currentStamina = Mathf.Clamp(currentStamina + _staminaCollectionAmount, 0, 100);
    }


}
