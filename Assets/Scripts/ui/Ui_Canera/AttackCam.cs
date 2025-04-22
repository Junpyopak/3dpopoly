using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCam : MonoBehaviour
{
    private float ShakeTime;
    private float ShakeIntensity;
    private static AttackCam instance;
    public static AttackCam Instance => instance;
    private CamFollowUi camFollowUi;
    public AttackCam() 
    {
        instance = this;
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown("1"))
    //    {
    //        AttackShakeCam(0.1F,0.3F);
    //    }
    //}
    private void Awake()
    {
        camFollowUi = GetComponent<CamFollowUi>();
    }
    public void AttackShakeCam(float ShakeTime = 1.0f, float ShakeIntensity =1.0f)
    {
        this.ShakeTime = ShakeTime;
        this.ShakeIntensity = ShakeIntensity;

        StartCoroutine("ShakeRota");
    }
    private IEnumerator ShakeRota()
    {
        camFollowUi.OnShake = true;
        Vector3 startRotation = transform.eulerAngles;
        while(ShakeTime>0.0f)
        {
            float ShakePower = 10f;
            float x = 0f;
            float y = 0f;
            float z = Random.Range(-1, 1);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * ShakeIntensity * ShakePower);

            ShakeTime-=Time.deltaTime;
            yield return null; 
        }
        transform.rotation = Quaternion.Euler(startRotation);
        camFollowUi.OnShake= false;
    }
}
