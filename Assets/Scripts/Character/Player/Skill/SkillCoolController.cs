using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolController : MonoBehaviour
{
    public Button skillButton;
    public Image cooldownOverlay;
    public float cooldownTime = 8f;

    private float cooldownTimer = 0f;
    private bool isCooldown = false;

    void Start()
    {
        cooldownOverlay.fillAmount = 0f;
        SetOverlayAlpha(0f); // ó���� �Ⱥ��̰�
        skillButton.onClick.AddListener(UseSkill);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isCooldown)
        {
            UseSkill();
        }

        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;

            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                cooldownOverlay.fillAmount = 0f;
                SetOverlayAlpha(0f); // ��Ÿ�� ������ �����ϰ�
                skillButton.interactable = true;
            }
        }
    }

    void UseSkill()
    {
        Debug.Log("��ų ���!");
        isCooldown = true;
        cooldownTimer = cooldownTime;
        skillButton.interactable = false;
        cooldownOverlay.fillAmount = 1f;
        SetOverlayAlpha(0.6f); // ��Ÿ�� �� �帮�� (0.6 ���� ��õ)
    }

    void SetOverlayAlpha(float alpha)
    {
        Color color = cooldownOverlay.color;
        color.a = alpha;
        cooldownOverlay.color = color;
    }
}
