using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data", order = 1)]

public class DataPlayer : ScriptableObject
{
  public string name;
  public float moveSpeedData;
  public float accelerationData;
}
