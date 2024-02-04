using Assets.Scripts.Managers;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public float effectCooldown = 5f;
    public FountainType fountainType;
    public Animator[] torchAnimators;

    public AnimationClip[] torchAnimationClips;

    private float startTime = 0f;
    private bool isFountainTriggered = false;

    private void Start()
    {
       TurnOffTorches();
    }


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
        EffectManager.Instance.ApplyFountainEffect(fountainType,startTime,effectCooldown,transform.position  + new Vector3( 0.25f,-0.25f,0f));
        TurnOnTorches();
        startTime = effectCooldown;
        isFountainTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EffectManager.Instance.CancelFountainEffect(fountainType);
        TurnOffTorches();

    }


    private void TurnOffTorches()
    {
        foreach (var animator in torchAnimators)
        {
            animator.Play(torchAnimationClips[0].name);
        }
    }

    private void TurnOnTorches()
    {
        foreach (var animator in torchAnimators)
        {
            animator.Play(torchAnimationClips[1].name);
        }
    }
}

public enum FountainType
{
    HealthRegeneration,
    ArmorRegeneration
}
