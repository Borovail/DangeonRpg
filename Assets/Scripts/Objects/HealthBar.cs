using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    private RectTransform healthBar;

    private void Awake()
    {
        healthBar = fillImage.gameObject. GetComponent<RectTransform>();
    }

    public void ChangeHealth(float currentHp,float maxHp)
    {
      fillImage.fillAmount = currentHp / maxHp; 
    }
}
