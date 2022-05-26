using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour
{
    public GameObject player;
    public GameObject damageAreaForJump;
    public Image healthBarFilling;
    public Animator animator;
    public GameObject effect;

    System.Random r = new System.Random();

    private int maxHealthPlayer = 300;
    private int currentHealthPlayer;
    private int randomAttack;

    private float currentHealthAsPercantagePlayer;
    private float timerPlayer;
    private float lastCallTimePlayer;
    private float bossAttack;
    private float lastCallBossAttack;
    private float startOfBossAttack;
    private float endOfBossAttack;
    private float tiredTime;

    private string attackName = "";

    private bool isChecked = false;    
    private bool isSuperAttackOn = false;
    private bool isBossTired = false;

    private void Start()
    {
        lastCallTimePlayer = 0;
        lastCallBossAttack = Time.time;
        currentHealthPlayer = maxHealthPlayer;
        animator = GetComponent<Animator>();
    }

    public void ChangeHealth(int value)
    {
        currentHealthPlayer -= value;
        if (currentHealthPlayer <= 0)
        {
            currentHealthAsPercantagePlayer = 0;
            Death();
        }
        else
        {
            currentHealthAsPercantagePlayer = (float)currentHealthPlayer / maxHealthPlayer;
        }
    }

    private void Death()
    {
        healthBarFilling.fillAmount = currentHealthAsPercantagePlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attackName == "MagnetAttackBoss" && isSuperAttackOn == true)
        {
            endOfBossAttack = Time.time + 1f;
            //ChangeHealth(20);
            animator.SetTrigger("MagnetOn");
            isSuperAttackOn = false;
            //animator.SetBool("MagnetBossAttack", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSuperAttackOn == false)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_BossTired"))
            {
                timerPlayer = Time.time;
                if (timerPlayer - lastCallTimePlayer >= 0.5f)
                {
                    effect.SetActive(true);
                    animator.SetBool("IsHitting", true);
                    lastCallTimePlayer = Time.time;
                    ChangeHealth(3);
                    healthBarFilling.fillAmount = currentHealthAsPercantagePlayer;
                }
            }
        }
        if (attackName == "MagnetAttackBoss" && isSuperAttackOn == true)
        {
            endOfBossAttack = Time.time + 1f;
            //ChangeHealth(20);
            animator.SetTrigger("MagnetOn");
            isSuperAttackOn = false;
            //animator.SetBool("MagnetBossAttack", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("IsHitting", false);
        effect.SetActive(false);
    }

    private void RAttack()
    {
        randomAttack = r.Next(0, 3);
        switch (randomAttack)
        {
            case 0:
                {
                    //animator.SetBool("MagnetBossAttack", true);
                    endOfBossAttack = Time.time + 5;
                    attackName = "MagnetAttackBoss";
                    break;
                }
            case 1:
                {
                    endOfBossAttack = Time.time + 5;
                    animator.SetBool("BossJumpAttack", true);
                    attackName = "BossJumpAttack";
                    break;
                }
            case 2:
                {
                    endOfBossAttack = Time.time + 3;
                    animator.SetBool("BossJumpAndHitAttack", true);
                    attackName = "BossJumpAndHitAttack";
                    break;
                }
        }
    }

    // Окончание супер атаки, отключение всех анимок и очищение переменных
    private void EndOfSuperAttack()
    {
        if (attackName == "BossJumpAttack")
        {
            damageAreaForJump.transform.Translate(0, -0.238f, 0);
        }
        animator.speed = 1f;
        animator.SetBool("BossJumpAttack", false);
        animator.SetBool("BossJumpAndHitAttack", false);
        animator.SetBool("MagnetBossAttack", false);
        attackName = "";
        isChecked = false;
        lastCallBossAttack = Time.time;
        isSuperAttackOn = false;
    }

    // Просто притягивание игрока
    private void MagnetAttack()
    {
        if (Time.timeScale != 0)
        {
            Vector3 touchpos = transform.position - player.transform.position;
            touchpos.Normalize();
            float moveposX = touchpos.x * Time.fixedDeltaTime * 0.3f;
            float moveposZ = touchpos.z * Time.fixedDeltaTime * 0.3f;
            player.transform.position += new Vector3(moveposX, 0, moveposZ);
        }
    }

    private void Update()
    {
        print(isSuperAttackOn);
        bossAttack = Time.time;
        if (bossAttack - lastCallBossAttack >= 5f && attackName=="")
        {
            effect.SetActive(false);
            isSuperAttackOn = true;
            isBossTired = true;
            //animator.GetCurrentAnimatorStateInfo(0).IsName("");
            animator.SetBool("IsHitting", false);
            animator.SetBool("IsBossIdle", true);
            RAttack();
        }
        if (attackName!="")
        {
            startOfBossAttack = Time.time;
            if (attackName == "MagnetAttackBoss")
            {
                MagnetAttack();
            }
            if (startOfBossAttack - endOfBossAttack >= 0)
            {
                EndOfSuperAttack();
            }
        }

    }
}
