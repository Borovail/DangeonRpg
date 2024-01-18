using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Animator swordStatueAnimator;
    public AnimationClip openAnimationClip;
    public AnimationClip closeAnimationClip;
    public GameObject player;
   
    private bool isPlayerInZone = false;
    private bool isAnimationEnded = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        if (isAnimationEnded)
            swordStatueAnimator.Play(openAnimationClip.name, -1, 0f);

        isPlayerInZone = true;
        isAnimationEnded = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != player) return;

        isPlayerInZone = false;

        StartCoroutine(WaitForAnimation());
    }




    private IEnumerator WaitForAnimation()
    {
        yield return new WaitUntil(() => swordStatueAnimator.GetCurrentAnimatorStateInfo(0).IsName(openAnimationClip.name));
        yield return new WaitUntil(() => swordStatueAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        isAnimationEnded = true;

        if (!isPlayerInZone)
            swordStatueAnimator.Play(closeAnimationClip.name, -1, 0f);
    }
}
