using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterBaseController
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] float attackInterval = 1f;

    protected float moveStep;
    protected float lookStep;
    private float deltaDistance;

    protected Transform target;
    protected CharacterBaseController targetController;
    
    protected bool canAttack;
    protected float currentAttackCoolDown;

    protected override void OnEnable()
    {
        base.OnEnable();

        attackInterval = 1f;
        currentAttackCoolDown = attackInterval;
        canAttack = true;

        target = CombatManager.Instance.Player.transform;
        targetController = target.GetComponent<CharacterBaseController>();
        
        agent.speed = characterData.MovementSpeed;
        agent.isStopped = false;
    }

    protected override void OnDisable()
    {
        ScoreManager.Instance.AddScore(baseCharacterData.score);
    }

    protected override void Update()
    {
        base.Update();

        if (CheckIsDead())
        {
            agent.isStopped = true;
            return;
        }

        UpdateAttackCoolDown();

        CalculateDeltaDistance();

        CheckReachTarget();
    }

    public override void MoveCharacter()
    {
        if (deltaDistance <= 0.5f) return;

        agent.SetDestination(target.position);
    }
    protected override void UpdateCharacterMoveAnimation()
    {
        characterView.SetMoveAnimation(agent.velocity.normalized);
    }

    private void CalculateDeltaDistance()
    {
        deltaDistance = Vector3.Distance(transform.position, target.position);
    }

    private void CheckReachTarget()
    {
        if (deltaDistance <= 1.5f)
        {
            OnAttack();
        }
    }

    public override void OnAttack()
    {
        if (!canAttack) return;

        characterData.OnAttack();
        characterView.OnAttack(null);

        currentAttackCoolDown = 0f;
        targetController.OnReceiveDamage(characterData.CurrentDamage);
    }

    public override void RotateCharacter()
    {

    }
    protected void UpdateAttackCoolDown()
    {
        if (currentAttackCoolDown < attackInterval)
        {
            currentAttackCoolDown += Time.deltaTime;
        }

        canAttack = currentAttackCoolDown >= attackInterval;
    }

    public override void OnReceiveDamage(int damage)
    {
        base.OnReceiveDamage(damage);
        SoundSystem.Instance.PlaySFX("ZombieSound");
    }
}
