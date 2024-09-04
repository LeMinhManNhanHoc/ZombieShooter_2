using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "ScriptableObject/Character Data", order = 0)]
public class BaseCharacterData : ScriptableObject
{
    public int MaxHP;
    public int Damage;
    public float MovementSpeed;
    public float RotateSpeed;
    public int Level;
    public int score;
}
