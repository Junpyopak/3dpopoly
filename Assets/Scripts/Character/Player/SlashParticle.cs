using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashParticle : MonoBehaviour
{
    public static SlashParticle instance;
    public ParticleSystem slashEffect;
    public Transform swordTip; // �� �� �κ� (�� ������Ʈ�� ����)
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
        // ����Ʈ�� ��� ���� ���� ��ġ�� ���� ������Ʈ
        if (slashEffect != null && slashEffect.isPlaying)
        {
            UpdateSlashEffect();
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ�� (���� �ִϸ��̼� ���� ������ �ֱ�)
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

    // �� ������ ��ġ�� ���� ������Ʈ
    private void UpdateSlashEffect()
    {
        slashEffect.transform.position = swordTip.position;

        Vector3 direction = (swordTip.position - lastSwordTipPos).normalized;
        if (direction != Vector3.zero)
            slashEffect.transform.rotation = Quaternion.LookRotation(direction);

        lastSwordTipPos = swordTip.position;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ�� (���� �ִϸ��̼� ���� ������ �ֱ�)
    public void EndSlashEffect()
    {
        if (slashEffect != null)
        {
            slashEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
