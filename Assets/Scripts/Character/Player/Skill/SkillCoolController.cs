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
        SetOverlayAlpha(0f); // 처음엔 안보이게
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
                SetOverlayAlpha(0f); // 쿨타임 끝나면 투명하게
                skillButton.interactable = true;
            }
        }
    }

    void UseSkill()
    {
        Debug.Log("스킬 사용!");
        isCooldown = true;
        cooldownTimer = cooldownTime;
        skillButton.interactable = false;
        cooldownOverlay.fillAmount = 1f;
        SetOverlayAlpha(0.6f); // 쿨타임 중 흐리게 (0.6 정도 추천)
    }

    void SetOverlayAlpha(float alpha)
    {
        Color color = cooldownOverlay.color;
        color.a = alpha;
        cooldownOverlay.color = color;
    }
}
