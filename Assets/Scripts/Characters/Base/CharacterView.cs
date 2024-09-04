using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] BoxCollider hitBox;
    [SerializeField] Animator myAnimator;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("UI")]
    [SerializeField] Image healthBarFiller;
    [SerializeField] Text healthBarText;

    #region Constant region
    const string MOVE_HORIZONTAL = "Move_Horizontal";
    const string MOVE_VERTICAL = "Move_Vertical";
    const string DEAD = "Dead";
    const string ATTACK = "Attack";
    #endregion

    Material[] mats;
    public void Init()
    {
        mats = skinnedMeshRenderer.materials;

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_DisolvePower", 0);
        }

        isDead = false;
        hitBox.enabled = !isDead;
    }

    public void SetAttackAnimation()
    {
        myAnimator.SetTrigger(ATTACK);
    }

    public void SetMoveAnimation(Vector3 moveDirection)
    {
        myAnimator.SetFloat(MOVE_HORIZONTAL, moveDirection.x);
        myAnimator.SetFloat(MOVE_VERTICAL, moveDirection.z);
    }

    public void OnReceiveDamage(int damage)
    {
        
    }

    public void OnAttack(object data)
    {
        myAnimator.SetTrigger(ATTACK);
    }

    bool isDead = false;
    public void OnDead()
    {
        if(isDead) return;

        isDead = true;
        myAnimator.SetTrigger(DEAD);

        hitBox.enabled = !isDead;

        StartCoroutine(StartDisolve());
    }

    public void OnEnableInviTime(bool enable)
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetFloat("_CanFlash", enable ? 1f: 0f);
        }
    }

    IEnumerator StartDisolve()
    {
        float power = 0f;

        while (power <= 1)
        {
            power += Time.deltaTime;

            for (int i = 0; i < mats.Length; i++)
            {
                mats[i].SetFloat("_DisolvePower", power);
            }

            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
    }

    public void UpdateUI(int currentHP, int maxHP)
    {
        if(isDead) return;

        if (healthBarFiller != null)
        {
            healthBarFiller.fillAmount = (float)currentHP / maxHP;
        }

        if(healthBarText != null)
        {
            healthBarText.text = string.Format("HP:{0}/{1}", currentHP, maxHP);
        }
    }

    [SerializeField] Image reloadProgress;
    [SerializeField] Text bulletCounter;

    public void UpdateBulletCounter(BaseGunController currentGunController)
    {
        bulletCounter.text = $"{currentGunController.CurrentBulletInClip} / {currentGunController.ClipSize}";
    }

    public void UpdateReloadProgress(BaseGunController currentGunController)
    {
        reloadProgress.fillAmount = currentGunController.ReloadPercent;
    }
}
