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

    public AnimationClip playerDieAnimation;


    private Animator UIAnimator;

    private void Awake()
    {
        UIAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
       GameManager.Instance.OnHealthChanged += (health,maxHealth) => UpdatePlayerAttribute(health,maxHealth ,playerHealthBar, playerHealthPrefab);
        GameManager.Instance.OnArmorChanged += (armor, maxArmor) => UpdatePlayerAttribute(armor,maxArmor, playerArmorBar, playerArmorPrefab);
        GameManager.Instance.OnPlayerCoinsChanged +=(amount)=> UpdatePlayerCoins(amount);
        GameManager.Instance.OnPlayerGetsEffect += (effectIconPrefab) => HandlePlayerGetsEffect(effectIconPrefab);
        GameManager.Instance.OnPlayerEffectEnds += (effectIcon) => HandlePlayerEffectEnds(effectIcon);
        GameManager.Instance.OnPlayerDie += () => UIAnimator.Play(playerDieAnimation.name,-1,0f) ;

        GameManager.Instance.LoadUserStart();
    }

    private void UpdatePlayerAttribute(int value,int maxValue, GridLayoutGroup bar, Image prefab)
    {
        int currentCount = bar.transform.childCount;
        int requiredCount = Mathf.Clamp(currentCount + value, 0, int.MaxValue);

        for (int i = currentCount; i != requiredCount;)
        {
            if (i < requiredCount)
            {
                if(i >= maxValue) return;

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
