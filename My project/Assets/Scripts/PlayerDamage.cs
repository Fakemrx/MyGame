using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public GameObject boss;
    private int maxHealthBoss = 300;
    private int currentHealthBoss;
    public Image healthBarFillingBoss;
    private float currentHealthAsPercantageBoss;
    private float timerBoss;
    private float lastCallTimeBoss;
    public static bool isHittingBoss;
    public GameObject effect;

    private void Start()
    {
        lastCallTimeBoss = 0;
        currentHealthBoss = maxHealthBoss;
        isHittingBoss = false;
    }

    public void ChangeHealthBoss(int value)
    {
        currentHealthBoss -= value;
        if (currentHealthBoss <= 0)
        {
            currentHealthAsPercantageBoss = 0;
            DeathBoss();
        }
        else
        {
            currentHealthAsPercantageBoss = (float)currentHealthBoss / maxHealthBoss;
        }
    }

    private void DeathBoss()
    {
        healthBarFillingBoss.fillAmount = currentHealthAsPercantageBoss;
    }

    private void OnTriggerStay(Collider other)
    {
        timerBoss = Time.time;
        isHittingBoss = true;
        if (timerBoss - lastCallTimeBoss >= 0.3f)
        {
            lastCallTimeBoss = Time.time;
            effect.SetActive(true);
            ChangeHealthBoss(5);
            healthBarFillingBoss.fillAmount = currentHealthAsPercantageBoss;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isHittingBoss = false;
        effect.SetActive(false);
    }
}
