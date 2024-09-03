using MessageBusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeGunController : BaseGunController
{
    [SerializeField] private int maxAmount = 5;
    [SerializeField] Text grenadeCounter;

    protected override void OnEnable()
    {
        MessageBus.Subsribe(MessageType.SHOOT_GRENADE, Shoot);
        grenadeCounter.text = maxAmount.ToString();
    }

    protected override void OnDisable()
    {
        MessageBus.Unsubscribe(MessageType.SHOOT_GRENADE, Shoot);
    }
    public override void Shoot(object data)
    {
        if (!canShoot || maxAmount <= 0) return;

        GameObject bullet = gunObjectPool.GetPooledObject();

        if (bullet == null) return;

        currentShootCD = 0f;

        bullet.transform.position = muzzleTransform.position;
        bullet.transform.localRotation = Quaternion.LookRotation(muzzleTransform.forward);

        bullet.SetActive(true);

        maxAmount--;
        grenadeCounter.text = maxAmount.ToString();

        if (vfxShoot != null) vfxShoot.Play(true);
    }
}
