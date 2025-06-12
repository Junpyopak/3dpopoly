using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolController : MonoBehaviour
{
    public static SkillCoolController instance;

    public Button skillButton;
    public Image cooldownOverlay;
    public float cooldownTime = 8f;

    public Button skill2Button;
    public Image Skill2cooldownOverlay;
    public float Skill2cooldownTime = 10f;

    private float cooldownTimer1 = 0f;
    private float cooldownTimer2 = 0f;
    private bool isCooldown1 = false;
    private bool isCooldown2 = false;

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
        SetOverlayAlpha(cooldownOverlay, 0f);
        SetOverlayAlpha(Skill2cooldownOverlay, 0f);

        skillButton.onClick.AddListener(UseSkill1);
        skill2Button.onClick.AddListener(UseSkill2);

        animator = GameObject.Find("character").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isCooldown1)
        {
            UseSkill1();
        }

        if (Input.GetKeyDown(KeyCode.L) && !isCooldown2)
        {
            UseSkill2();
        }

        if (isCooldown1)
        {
            cooldownTimer1 -= Time.deltaTime;
            cooldownOverlay.fillAmount = 1f - (cooldownTimer1 / cooldownTime);

            if (cooldownTimer1 <= 0f)
            {
                isCooldown1 = false;
                cooldownOverlay.fillAmount = 0f;
                SetOverlayAlpha(cooldownOverlay, 0f);
                skillButton.interactable = true;
            }
        }

        if (isCooldown2)
        {
            cooldownTimer2 -= Time.deltaTime;
            Skill2cooldownOverlay.fillAmount = 1f - (cooldownTimer2 / Skill2cooldownTime);

            if (cooldownTimer2 <= 0f)
            {
                isCooldown2 = false;
                Skill2cooldownOverlay.fillAmount = 0f;
                SetOverlayAlpha(Skill2cooldownOverlay, 0f);
                skill2Button.interactable = true;
            }
        }
    }

    void UseSkill1()
    {
        Debug.Log("Skill 1 사용!");
        animator.SetTrigger("Skill1");

        isSkillCasting = true;
        isCooldown1 = true;
        cooldownTimer1 = cooldownTime;

        skillButton.interactable = false;
        cooldownOverlay.fillAmount = 1f;
        SetOverlayAlpha(cooldownOverlay, 0.6f);
    }

    void UseSkill2()
    {
        Debug.Log("Skill 2 사용!");
        animator.SetTrigger("Skill2");

        isSkillCasting = true;
        isCooldown2 = true;
        cooldownTimer2 = Skill2cooldownTime;

        skill2Button.interactable = false;
        Skill2cooldownOverlay.fillAmount = 1f;
        SetOverlayAlpha(Skill2cooldownOverlay, 0.6f);
    }

    void SetOverlayAlpha(Image overlay, float alpha)
    {
        Color color = overlay.color;
        color.a = alpha;
        overlay.color = color;
    }
}
