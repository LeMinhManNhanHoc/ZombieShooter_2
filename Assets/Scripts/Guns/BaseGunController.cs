using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessageBusSystem;

public class BaseGunController : MonoBehaviour
{
    [SerializeField] protected float shootInterval = 1f;
    [SerializeField] protected ObjectPool gunObjectPool;
    [SerializeField] protected ParticleSystem vfxShoot;
    [SerializeField] protected Transform muzzleTransform;

    [SerializeField] protected int clipSize = 10;
    [SerializeField] protected float reloadTime = 2f;
    protected int currentBulletInClip;
    protected float currentReloadTime;
    protected bool isReloading = false;

    protected float currentShootCD;
    protected bool canShoot;

    public int CurrentBulletInClip { get { return currentBulletInClip; } }
    public int ClipSize { get { return clipSize; } }

    public float ReloadPercent { get { return currentReloadTime / reloadTime; } }

    void Awake()
    {
        gunObjectPool.InitPool();

        currentShootCD = shootInterval;

        currentBulletInClip = clipSize;

        currentReloadTime = reloadTime;
    }

    protected virtual void OnEnable()
    {
        MessageBus.Subsribe(MessageType.SHOOT, Shoot);
    }

    protected virtual void OnDisable()
    {
        MessageBus.Unsubscribe(MessageType.SHOOT, Shoot);
    }

    private void Update()
    {
        UpdateShootCoolDown();
        UpdateReloadTime();
    }

    public virtual void Shoot(object data)
    {
        if (!canShoot || isReloading) return;

        GameObject bullet = gunObjectPool.GetPooledObject();

        if (bullet == null) return;

        currentShootCD = 0f;

        bullet.transform.position = muzzleTransform.position;
        bullet.transform.localRotation = Quaternion.LookRotation(muzzleTransform.forward);

        bullet.SetActive(true);

        if(vfxShoot != null) vfxShoot.Play(true);

        SoundSystem.Instance.PlaySFX("Shoot");

        currentBulletInClip--;

        if(currentBulletInClip <= 0)
        {
            BeginReload();
        }

        MessageBus.Announce(MessageType.DONE_SHOOT, null);
    }

    protected void UpdateShootCoolDown()
    {
        if (currentShootCD < shootInterval)
        {
            currentShootCD += Time.deltaTime;
        }

        canShoot = currentShootCD >= shootInterval && currentBulletInClip > 0;
    }

    protected void UpdateReloadTime()
    {
        if (currentReloadTime < reloadTime)
        {
            isReloading = true;
            currentReloadTime += Time.deltaTime;
        }
        else if(isReloading)
        {
            isReloading = false;
            currentBulletInClip = clipSize;
        }
    }

    public void BeginReload()
    {
        if (isReloading) return;

        currentBulletInClip = 0;
        currentReloadTime = 0f;
        SoundSystem.Instance.PlaySFX("Reload");
    }
}
