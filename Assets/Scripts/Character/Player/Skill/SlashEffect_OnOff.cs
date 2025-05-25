using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect_OnOff : MonoBehaviour
{
    public static SlashEffect_OnOff instance;
    public ParticleSystem SlashEffect;
    public float EffectDuration = 0.1f;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void PlayEffect()
    {
        if (SlashEffect != null)
        {

            SlashEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            SlashEffect.Play();
            StopAllCoroutines();
            StartCoroutine(DisEffect());
        }
    }
    private System.Collections.IEnumerator DisEffect()
    {
        yield return new WaitForSeconds(EffectDuration);
        if (SlashEffect != null)
        {
            SlashEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
