using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect_OnOff : MonoBehaviour
{
    public static SlashEffect_OnOff instance;
    public ParticleSystem SlashEffect;
    public Transform swordEnd;
    private void Awake()
    {
        SlashEffect = GetComponent<ParticleSystem>();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Slash_On()
    {
        if (swordEnd != null && SlashEffect != null)
        {
            Transform effectTransform = SlashEffect.transform;

            effectTransform.position = swordEnd.position;
            effectTransform.rotation = swordEnd.rotation; 

            SlashEffect.Play();
        }
    }
    public void Slash_Off()
    {
        if (SlashEffect != null)
            SlashEffect.Stop();
    }
}
