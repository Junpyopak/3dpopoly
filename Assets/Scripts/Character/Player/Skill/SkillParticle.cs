using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParticle : MonoBehaviour
{
    public float radius = 0.5f;

    public ParticleSystem skillParticle;

    void Awake()
    {
        if (skillParticle == null)
            skillParticle = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        Debug.Log("작동중");
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("파티클 충돌");
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<AIMonster>();
            enemy.Damage();
        }
    }
}
