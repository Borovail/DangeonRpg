using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HealingArmorFountain : MonoBehaviour
{
    public float healingCooldown = 5f;
    public GameObject healingEffect;

    private float startTime = 0f;
    private bool isFountainTriggered = false;



    private void Update()
    {
        if (isFountainTriggered)
        {
            startTime -= Time.deltaTime;

            if (startTime <= 0)
            {
                startTime = 0;
                isFountainTriggered = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeRepeating("GiveHealingArmorEffect", startTime, healingCooldown);
        GameManager.Instance.PlayerGetsEffect(healingEffect);
        startTime = 5f;
        isFountainTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke();
        GameManager.Instance.PlayerEffectEnds(healingEffect);
    }

    private void GiveHealingArmorEffect()
    {
        GameManager.Instance.PlayerArmorChanged(1);
        FloatingTextManager.Instance.Show(new FloatingTextSettings("+1", 3, 16, Color.blue, Vector3.up * 80, transform.position, FloatingTextType.UIRelativeFloatingText));
    }

}

