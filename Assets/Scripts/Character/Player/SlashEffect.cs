using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [Header("이펙트 설정")]
    public GameObject slashPrefab;         
    public Transform swordTip;             
    public float destroyDelay = 0.5f;     
    public Vector3 effectRotationOffset;   

    [Header("디버그")]
    public KeyCode testKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(testKey))
        {
            Debug.Log("공격 입력 감지됨");
            SpawnSlashEffect();
        }
    }

    public void SpawnSlashEffect()
    {
        if (slashPrefab == null || swordTip == null)
        {
            Debug.LogWarning("slashPrefab 또는 swordTip이 연결되지 않았습니다.");
            return;
        }

        // 회전 방향 = 검이 향하는 방향 + 보정값
        //Quaternion rotation = swordTip.rotation * Quaternion.Euler(effectRotationOffset);

        // 이펙트 생성
        //GameObject fx = Instantiate(slashPrefab, swordTip.position, rotation);
        GameObject fx = Instantiate(slashPrefab, swordTip.position, Quaternion.identity);
        Debug.Log("이펙트 생성 완료");
        // 자동 삭제
        Destroy(fx, destroyDelay);
    }
}
