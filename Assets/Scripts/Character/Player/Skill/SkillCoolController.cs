using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolController : MonoBehaviour
{
    public static SkillCoolController instance;
    public Button skillButton;
    public Image cooldownOverlay;
    public Image Skill2cooldownOverlay;
    public float cooldownTime = 8f;

    private float cooldownTimer = 0f;
    private bool isCooldown = false;
    Animator animator;
    public bool isSkillCasting = false;
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
        cooldownOverlay.fillAmount = 0f;
        Skill2cooldownOverlay.fillAmount = 0f;
        SetOverlayAlpha(0f);
        skillButton.onClick.AddListener(UseSkill);
        animator = GameObject.Find("character").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isCooldown)
        {
            UseSkill();
        }
        if(Input.GetKeyDown(KeyCode.L) && !isCooldown)
        {
            UseSkill2();
        }

        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownOverlay.fillAmount = 1f -(cooldownTimer / cooldownTime);

            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                cooldownOverlay.fillAmount = 0f;
                Skill2cooldownOverlay.fillAmount = 0f;
                SetOverlayAlpha(0f);
                skillButton.interactable = true;
            }
        }
    }

    void UseSkill()
    {
        Debug.Log("��ų ���!");
        animator.SetTrigger("Skill1");
        isSkillCasting = true;
        isCooldown = true;
        cooldownTimer = cooldownTime;
        skillButton.interactable = false;
        cooldownOverlay.fillAmount = 1f;
        SetOverlayAlpha(0.6f);
    }
    void UseSkill2()
    {
        animator.SetTrigger("Skill2");
        isSkillCasting = true;
        isCooldown = true;
        cooldownTimer = cooldownTime;
        Skill2cooldownOverlay.fillAmount = 1f;
    }

    void SetOverlayAlpha(float alpha)
    {
        Color color = cooldownOverlay.color;
        color.a = alpha;
        cooldownOverlay.color = color;
    }
   
}
