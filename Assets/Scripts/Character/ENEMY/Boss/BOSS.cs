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
    [SerializeField] List<GameObject> listPattern;//패턴의 종류
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
    }

    private void Attack_1()
    {
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
        if(Attack2 == true)
        {
            Shared.MainShake.Shake(0);
            StartCoroutine(RoarCoroutine());
            Vector3 Pos = new Vector3(0, 0.01f, 4.21f);
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
            Debug.Log("데미지");
            Debug.Log($"스탯{HP}");
        }
    }

    public override void Damage()
    {
        HP -= PlayerDamage;
        if (HP <=0)
        {
            Hp = 0;
            BossHpgauge.SetHp(HP, MaxHp);
            Destroy(gameObject);
            Debug.Log("보스가 죽었습니다");
        }
    }

}
