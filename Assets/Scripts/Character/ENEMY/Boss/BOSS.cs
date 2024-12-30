using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : Enemy
{
    // Start is called before the first frame update
    public bool Attack1 = false;
    public bool Attack2;
    public int HP = 300;
    public int MaxHp = 300;
    [SerializeField] BossHpGauge BossHpgauge;
    [SerializeField] List<GameObject> listPattern;//������ ����
    void Start()
    {
        HP = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Attack_1();
    }

    private void Attack_1()
    {
        if (Attack1 == true)
        {
            Vector3 Pos = transform.position;
            GameObject pattern = Instantiate(listPattern[0], Pos, Quaternion.identity);
            pattern.name = "Range";
            Attack1 = false;
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
        if (HP <=0)
        {
            Hp = 0;
            BossHpgauge.SetHp(HP, MaxHp);
            Destroy(gameObject);
            Debug.Log("������ �׾����ϴ�");
        }
    }

}
