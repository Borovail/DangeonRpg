using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{
    public float healingCooldown = 5f;
    public GameObject healingEffect;

    private float startTime=0f;
    private bool isFountainTriggered = false;



    private void Update()
    {
        if(isFountainTriggered)
        {
            startTime -= Time.deltaTime;
           
            if(startTime <= 0)
            {
                startTime = 0;
                isFountainTriggered = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       InvokeRepeating("GiveHealingHealthEffect", startTime, healingCooldown);
        GameManager.Instance.PlayerGetsEffect(healingEffect);
        startTime = 5f;
        isFountainTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke();
        GameManager.Instance.PlayerEffectEnds(healingEffect);
    }

    private void GiveHealingHealthEffect()
    {
        GameManager.Instance.PlayerHealthChanged(1);
        FloatingTextManager.Instance.Show(new FloatingTextSettings("+1", 3, 16, Color.green, Vector3.up * 80, transform.position, FloatingTextType.UIRelativeFloatingText));
    }

}
