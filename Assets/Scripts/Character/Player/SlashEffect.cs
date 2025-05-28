using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [Header("����Ʈ ����")]
    public GameObject slashPrefab;         
    public Transform swordTip;             
    public float destroyDelay = 0.5f;     
    public Vector3 effectRotationOffset;   

    [Header("�����")]
    public KeyCode testKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(testKey))
        {
            Debug.Log("���� �Է� ������");
            SpawnSlashEffect();
        }
    }

    public void SpawnSlashEffect()
    {
        if (slashPrefab == null || swordTip == null)
        {
            Debug.LogWarning("slashPrefab �Ǵ� swordTip�� ������� �ʾҽ��ϴ�.");
            return;
        }

        // ȸ�� ���� = ���� ���ϴ� ���� + ������
        //Quaternion rotation = swordTip.rotation * Quaternion.Euler(effectRotationOffset);

        // ����Ʈ ����
        //GameObject fx = Instantiate(slashPrefab, swordTip.position, rotation);
        GameObject fx = Instantiate(slashPrefab, swordTip.position, Quaternion.identity);
        Debug.Log("����Ʈ ���� �Ϸ�");
        // �ڵ� ����
        Destroy(fx, destroyDelay);
    }
}
