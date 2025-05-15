using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Attack3 : MonoBehaviour
{
    public GameObject Range;
    float speed = 0.2f;
    Vector3 Vector3 = new Vector3(15,15,0);
    ParticleSystem BossSkill3;
    // Start is called before the first frame update
    void Start()
    {
        BossSkill3 = GameObject.Find("Skill3").GetComponent<ParticleSystem>();
        Invoke("EndRange", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Size();

    }
    //IEnumerator SkillStartCoroutine()
    //{
    //    yield return new WaitForSeconds(1f);
    //    BossSkill3.Play();
    //}
    void Size()
    {
        if (Range.transform.localScale.x<=Vector3.x && Range.transform.localScale.y<=Vector3.y)
        {
            Range.transform.localScale += new Vector3(0.3f, 0.3f, 0) * speed;
            //StartCoroutine(SkillStartCoroutine());
            if (Range.transform.localScale.x == Vector3.x && Range.transform.localScale.y == Vector3.y)
            {
                BossSkill3.Play();
            }
        }
    }
    void EndRange()
    {

        Destroy(Range);
    }
}
