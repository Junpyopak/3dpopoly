using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashParticle : MonoBehaviour
{
    public static SlashParticle instance;
    public ParticleSystem slashEffect;
    public Transform swordTip; // 검 끝 부분 (빈 오브젝트로 지정)
    public string[] attackAnimationNames = { "Attack1", "Attack2" };

    private Animator animator;
    private Vector3 lastSwordTipPos;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        lastSwordTipPos = swordTip.position;
    }

    void Update()
    {
        // 이펙트가 재생 중일 때만 위치와 방향 업데이트
        if (slashEffect != null && slashEffect.isPlaying)
        {
            UpdateSlashEffect();
        }
    }

    // 애니메이션 이벤트에서 호출 (공격 애니메이션 시작 시점에 넣기)
    public void StartSlashEffect()
    {
        if (slashEffect != null)
        {
            slashEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            slashEffect.Clear();

            slashEffect.transform.position = swordTip.position;

            Vector3 direction = (swordTip.position - lastSwordTipPos).normalized;
            if (direction != Vector3.zero)
                slashEffect.transform.rotation = Quaternion.LookRotation(direction);

            slashEffect.Play();
        }

        lastSwordTipPos = swordTip.position;
    }

    // 매 프레임 위치와 방향 업데이트
    private void UpdateSlashEffect()
    {
        slashEffect.transform.position = swordTip.position;

        Vector3 direction = (swordTip.position - lastSwordTipPos).normalized;
        if (direction != Vector3.zero)
            slashEffect.transform.rotation = Quaternion.LookRotation(direction);

        lastSwordTipPos = swordTip.position;
    }

    // 애니메이션 이벤트에서 호출 (공격 애니메이션 종료 시점에 넣기)
    public void EndSlashEffect()
    {
        if (slashEffect != null)
        {
            slashEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
