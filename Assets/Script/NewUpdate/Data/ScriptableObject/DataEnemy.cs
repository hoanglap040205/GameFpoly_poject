using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data", order = 2)]

public class DataEnemy : ScriptableObject
{
    public string name;
    public float radiusDat;
    public float moveSpeedData;
    public float speedMaxData;
    
}
