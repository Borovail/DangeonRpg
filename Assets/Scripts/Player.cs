using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player : MonoBehaviour
{
    public GameObject swordObject;

    public float speed = 2f;

    public event Action OnAttackEnd;

    private bool isAttacking = false;
    private float originalXScale;
    private SpriteRenderer swordRenderer;
    private Sword sword;

    private IInteractable interactableGameObject;

    private void Awake()
    {
        originalXScale = transform.localScale.x;
        swordRenderer = swordObject.GetComponent<SpriteRenderer>();
        sword = swordObject.GetComponent<Sword>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            interactableGameObject?.Interact();
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isAttacking) 
        {
          sword.Attack();
            isAttacking = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FloatingTextManager.instance.Show(new FloatingTextSettings("Pidor Up", 5f ,12,Color.red, Vector3.up,transform.position,FloatingTextType.UIRelativeFloatingText));   
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FloatingTextManager.instance.Show(new FloatingTextSettings("Pidor Down", 5f, 12, Color.red, Vector3.down, transform.position, FloatingTextType.WorldSpaceFloatingText )); 
        }
    }

    private void FixedUpdate()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        int y = (int)Input.GetAxisRaw("Vertical");   

        Vector3 direction = new Vector3(x, y, 0);

        if (x != 0)
        {
            transform.localScale = new Vector3(originalXScale* Mathf.Sign(x), transform.localScale.y, transform.localScale.z);
        }

        transform.position+= direction* speed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        var interactable = collision.gameObject.GetComponent<IInteractable>();

            if(interactable != null && interactableGameObject != interactable)
            interactableGameObject = interactable;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactableGameObject = null;
    }



    public void BuySword()
    {
        GameManager.instance.playerCoins -= GameManager.instance.swords[GameManager.instance.currentSwordId].price;
        UpdateSwordSkin();
    }
    private void UpdateSwordSkin()
    {
        swordRenderer.sprite = GameManager.instance.swords[GameManager.instance.currentSwordId].skin;
    }

   
    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
        OnAttackEnd?.Invoke();
    }
}
