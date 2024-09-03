using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageBusSystem;

public class PlayerController : CharacterBaseController
{
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected BaseGunController currentGunController;

    public float PlayerRemainHP { get { return characterData.CurrentHealth / characterData.MaxHealthPoint * 100f; } }

    protected Vector3 moveDirection;
    protected Vector3 lookDirection;

    protected Vector2 inputMoveDirection;
    protected Vector2 inputLookDirection;

    protected float moveStep;
    protected float lookStep;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnSwapWeapon(0);
        MessageBus.Subsribe(MessageType.DONE_SHOOT, (data) => characterView.OnAttack());
    }

    protected override void OnDisable()
    {
        MessageBus.Unsubscribe(MessageType.DONE_SHOOT, (data) => characterView.OnAttack());
    }

    public override void MoveCharacter()
    {
        inputMoveDirection = InputManager.Instance.GetMovementDirection();

        moveDirection = new Vector3(inputMoveDirection.x, 0f, inputMoveDirection.y);

        moveStep = characterData.MovementSpeed * Time.deltaTime;

        characterController.Move(moveDirection * moveStep);
    }

    public override void RotateCharacter()
    {
        inputLookDirection = InputManager.Instance.GetLookDirection();

        lookDirection = new Vector3(inputLookDirection.x, 0f, inputLookDirection.y) + rotateObject.forward;

        lookStep = Time.deltaTime * characterData.RotationSpeed;

        rotateObject.transform.rotation = Quaternion.Slerp(rotateObject.transform.rotation, Quaternion.LookRotation(lookDirection), lookStep);
    }

    protected override void UpdateCharacterMoveAnimation()
    {
        dotMoveLookDirection = Vector3.Dot(moveDirection, lookDirection);

        animMoveDirection = rotateObject.forward * dotMoveLookDirection + moveDirection;

        characterView.SetMoveAnimation(animMoveDirection.normalized);
    }

    private void CheckAttackFromInput()
    {
        if (inputLookDirection.magnitude >= 1)
        {
            OnAttack();
        }
    }

    public override void OnAttack()
    {
        MessageBus.Announce(MessageType.SHOOT, null);
    }

    protected override void Update()
    {
        base.Update();

        if (CheckIsDead()) return;

        CheckAttackFromInput();

        ApplyGravity();

        characterView.UpdateBulletCounter(currentGunController);
        characterView.UpdateReloadProgress(currentGunController);
    }

    float gravity = 9.8f;

    private Vector3 playerVelocity;
    protected void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            playerVelocity.y = 0;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    [SerializeField] GameObject[] weaponList;
    public void OnSwapWeapon(int index)
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            if (i == index)
            {
                weaponList[i].SetActive(true);
                currentGunController = weaponList[i].GetComponent<BaseGunController>();
            }
            else
            {
                weaponList[i].SetActive(false);
            }
        }
    }

    public void OnShootGrenade()
    {
        MessageBus.Announce(MessageType.SHOOT_GRENADE, null);
    }

    public override void OnReceiveDamage(int damage)
    {
        base.OnReceiveDamage(damage);
        SoundSystem.Instance.PlaySFX("PlayerHurt");
    }

    public void OnReload()
    {
        currentGunController.BeginReload();
    }
}
