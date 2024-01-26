using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerCoins;

    public GridLayoutGroup playerHealthBar;
    public Image playerHealthPrefab;
    public GridLayoutGroup playerArmorBar;
    public Image playerArmorPrefab;
    public GridLayoutGroup playerEffectsBar;
    public GameObject HpEffect;
    public GameObject ArmorEffect;


    private void Start()
    {
       GameManager.Instance.OnHealthChanged += (health) => UpdatePlayerAttribute(health, playerHealthBar, playerHealthPrefab);
        GameManager.Instance.OnArmorChanged += (armor) => UpdatePlayerAttribute(armor, playerArmorBar, playerArmorPrefab);
        GameManager.Instance.OnPlayerCoinsChanged +=(amount)=> UpdatePlayerCoins(amount);
        GameManager.Instance.OnPlayerGetsEffect += (effectIconPrefab) => HandlePlayerGetsEffect(effectIconPrefab);
        GameManager.Instance.OnPlayerEffectEnds += (effectIcon) => HandlePlayerEffectEnds(effectIcon);


    }

    private void UpdatePlayerAttribute(int value, GridLayoutGroup bar, Image prefab)
    {
        int currentCount = bar.transform.childCount;
        int requiredCount = Mathf.Clamp(currentCount + value, 0, int.MaxValue);

        for (int i = currentCount; i != requiredCount;)
        {
            if (i < requiredCount)
            {
                Instantiate(prefab, bar.transform);
                i++;
            }
            else
            {
                Destroy(bar.transform.GetChild(--i).gameObject);
            }
        }
    }

    private void HandlePlayerGetsEffect(GameObject effectIcon)
    {
        effectIcon.SetActive(true);
        effectIcon.transform.SetAsFirstSibling();
    }

    private void HandlePlayerEffectEnds(GameObject effectIcon)
    {
        effectIcon.SetActive(false);
    }





    private void UpdatePlayerCoins(int coins)
    {
        playerCoins.text =(int.Parse(playerCoins.text)+ coins).ToString();
    }
}
