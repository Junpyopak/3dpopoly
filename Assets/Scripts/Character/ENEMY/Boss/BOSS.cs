using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : Enemy
{
    // Start is called before the first frame update
    public bool Attack1 = false;
    public bool Attack2 = false;
    public bool Attack3 = false;
    public int HP = 300;
    public int MaxHp = 300;
    Animator Animator;
    [SerializeField] BossHpGauge BossHpgauge;
    [SerializeField] List<GameObject> listPattern;//������ ����
    public ParticleSystem MyparticleSystem;
    [Header("������ų ��Ÿ��")]
    [SerializeField] float CoolTime = 5.0f;
    float CoolTimer = 0.0f;
    void Start()
    {
        HP = MaxHp;
        Animator = GetComponent<Animator>();
    }

    IEnumerator RoarCoroutine()
    {
        yield return new WaitForSeconds(0f);
        Animator.SetTrigger("Roar");
    }

    // Update is called once per frame
    void Update()
    {
        Attack_1();
        Attack_2();
        Attack_3();
        if(Attack1==false)
        {
            CheckCool();
        }   
    }

    private void Attack_1()
    {
        if (CoolTime <= 0)
        {
            Attack1 = true;
            CoolTime = CoolTimer;
            CoolTime = 10f;
        }
        if (Attack1 == true)
        {
            Shared.MainShake.Shake(0);
            StartCoroutine(RoarCoroutine());
            Vector3 Pos = new Vector3(0, 0.01f, 4.21f);
            GameObject pattern = Instantiate(listPattern[0], Pos, Quaternion.identity);
            pattern.name = "Range";
            Attack1 = false;
        }
    }
    private void Attack_2()
    {
        if (Attack2 == true)
        {
            Shared.MainShake.Shake(0);
            StartCoroutine(RoarCoroutine());
            Vector3 Pos = new Vector3(0, 0.01f, transform.position.z-6.42f);
            GameObject pattern = Instantiate(listPattern[1], Pos, Quaternion.identity);
            pattern.name = "Range";
            Attack2 = false;
        }
    }
    private void Attack_3()
    {
        if (Attack3 == true)
        {
            Shared.MainShake.Shake(0);
            Vector3 Pos = transform.position;
            GameObject pattern = Instantiate(listPattern[2], Pos, Quaternion.identity);
            pattern.name = "Range";
            Attack3 = false;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sward"))
        {
            Damage();
            Debug.Log("������");
            Debug.Log($"����{HP}");
        }
    }

    public override void Damage()
    {
        HP -= PlayerDamage;


        if (HP <= 0)
        {
            Hp = 0;
            BossHpgauge.SetHp(HP, MaxHp);
            Destroy(gameObject);
            Debug.Log("������ �׾����ϴ�");
        }
    }

    public void StartBless()
    {
        MyparticleSystem.Play();
    }
    public void EndBless()
    {
        MyparticleSystem.Stop();
    }
    private void CheckCool()
    {
        if (CoolTime > 0)
        {
            CoolTime -= Time.deltaTime * 0.5f;

        }
    }
}
