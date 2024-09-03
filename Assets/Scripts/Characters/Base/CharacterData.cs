using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    private CharacterBaseController characterController;
    protected float movementSpeed;
    protected float rotateSpeed;

    private int maxHealthPoint;
    private int currentDamage;
    private int currentHealth;

    public bool IsDead {  get { return currentHealth <= 0; } }
    public int MaxHealthPoint {  get { return maxHealthPoint; } }
    public int CurrentHealth { get { return currentHealth; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float RotationSpeed { get {return rotateSpeed; } }
    public int CurrentDamage { get { return currentDamage; } }

    public void Init(CharacterBaseController characterController, BaseCharacterData data)
    {
        this.characterController = characterController;

        maxHealthPoint = data.MaxHP;
        currentDamage = data.Damage;
        movementSpeed = data.MovementSpeed;
        rotateSpeed = data.RotateSpeed;

        currentHealth = maxHealthPoint;
    }

    public void OnReceiveDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealthPoint);
    }

    public void OnDead()
    {
        
    }

    public void OnAttack()
    {

    }
}
