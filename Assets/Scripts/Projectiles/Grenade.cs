using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Grenade : BaseProjectileController
{
    [SerializeField] Rigidbody myRigidbody;
    bool isInit = false;

    protected override void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if(!isInit)
        {
            isInit = true;
            return;
        }
        
        GetAllInRange();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.AddForce(transform.forward * 5f, ForceMode.Impulse);
    }

    protected override void Update()
    {
        
    }

    private void GetAllInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 2f, LayerMask.GetMask("Enemy"));

        ParticleSystem vfx = VFXManager.Instance.GetVFXPool("Explode");
        Transform vfxTransform = vfx.transform;
        vfxTransform.position = transform.position;
        vfxTransform.gameObject.SetActive(true);

        vfx.Play();
        SoundSystem.Instance.PlaySFX("Grenade");

        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponent<EnemyController>().OnReceiveDamage(bulletDamage);
        }
    }
}
