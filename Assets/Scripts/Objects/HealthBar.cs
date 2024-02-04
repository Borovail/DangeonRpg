using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    private Monster parentMonster;

    private void Start()
    {
        parentMonster = GetComponentInParent<Monster>();
        parentMonster.OnHealthChanged += ChangeHealth;
    }

    public void ChangeHealth(int currentHp,int maxHp)
    {
      fillImage.fillAmount = (float)currentHp / (float)maxHp; 
    }

    private void OnDestroy()
    {
        if (parentMonster != null)
        {
            parentMonster.OnHealthChanged -= ChangeHealth;
        }
    }
}
