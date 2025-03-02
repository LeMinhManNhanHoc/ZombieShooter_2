using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectileController : MonoBehaviour
{
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletDuration;
    [SerializeField] TrailRenderer trailRenderer;

    WaitForSeconds coroutine;

    private void Awake()
    {
        coroutine = new WaitForSeconds(bulletDuration);
    }

    protected virtual void OnEnable()
    {
        trailRenderer.Clear();
        StartCoroutine(AutoDestroy());
    }

    protected virtual void OnDisable()
    {
        StopCoroutine(AutoDestroy());
    }

    protected virtual void Update()
    {
        transform.localPosition += transform.forward * bulletSpeed * Time.deltaTime;
    }

    IEnumerator AutoDestroy()
    {
        yield return coroutine;

        gameObject.SetActive(false);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().OnReceiveDamage(bulletDamage);
        }

        gameObject.SetActive(false);
    }
}
