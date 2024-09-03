using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseController : MonoBehaviour
{
    [SerializeField] protected Transform rotateObject;
    [SerializeField] protected CharacterView characterView;
    [SerializeField] protected BaseCharacterData baseCharacterData;
    [SerializeField] protected float maxInviTime = 0.1f;

    protected CharacterData characterData = new CharacterData();

    protected Vector3 animMoveDirection;
    protected float dotMoveLookDirection;

    protected float currentHitCD;
    protected bool canReceiveHit;

    //Unity function
    protected virtual void OnEnable()
    {
        characterData.Init(this, baseCharacterData);
        characterView.Init();

        currentHitCD = maxInviTime;
        canReceiveHit = true;
    }

    protected virtual void OnDisable()
    {
        
    }

    protected virtual void Update()
    {
        ReceiveDamageCoolDown();
        EnableInviTime();

        if (CheckIsDead()) return;

        MoveCharacter();
        RotateCharacter();

        UpdateCharacterMoveAnimation();

        characterView.UpdateUI(characterData.CurrentHealth, characterData.MaxHealthPoint);
    }

    //Custom function
    public virtual void OnReceiveDamage(int damage)
    {
        if (!canReceiveHit) return;

        canReceiveHit = false;
        currentHitCD = 0f;

        characterData.OnReceiveDamage(damage);
        characterView.OnReceiveDamage(damage);
    }

    public abstract void OnAttack();

    public virtual bool CheckIsDead()
    {
        if (characterData.IsDead)
        {
            characterView.OnDead();
            characterData.OnDead();
        }

        return characterData.IsDead;
    }

    public abstract void MoveCharacter();

    public abstract void RotateCharacter();

    protected abstract void UpdateCharacterMoveAnimation();

    protected virtual void ReceiveDamageCoolDown()
    {
        if(currentHitCD < maxInviTime)
        {
            currentHitCD += Time.deltaTime;
        }

        canReceiveHit = currentHitCD >= maxInviTime;
    }

    protected void EnableInviTime()
    {
        characterView.OnEnableInviTime(!canReceiveHit);
    }
}
