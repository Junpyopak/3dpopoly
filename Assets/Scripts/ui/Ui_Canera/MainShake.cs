using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShake : MonoBehaviour
{
    bool CameraShake = false;

    Transform ShakeTr;

    public class cShakeInfo
    {
        public float StartDelay;
        public bool UseTotalTime;
        public float TotalTime;
        public Vector3 Dest;//»ÁµÈ∏± πÊ«‚
        public Vector3 Shake;
        public Vector3 Dir;

        public float RemainDist;
        public float RemainCountDis;

        public bool UseCount;
        public int Count;

        public float Veclocity;

        public bool UseDamping;
        public float Damping;
        public float DampingTine;
    }
    cShakeInfo shakeInfo = new cShakeInfo();

    Vector3 OrgPos;
    float FovX = 0.2f;
    float FovY = 0.2f;

    float Left = 1.0f;
    float Right = -1.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        Shared.MainShake = this;

        OrgPos = transform.position;

        InitShake();
    }

    void InitShake()
    {
        ShakeTr = transform.parent;

        CameraShake = false;
    }

    void ResetShakeTr()
    {
        ShakeTr.localPosition = Vector3.zero;
        CameraShake = false;

        CameraLimit();

    }

    void CameraLimit(bool _OrgPosY = false)
    {
        Vector3 camera = OrgPos;

        if (camera.x - FovX < Left)
            camera.x = Left + FovX;

        else if (camera.x + FovX > Right)
            camera.x = Right - FovX;

        if (_OrgPosY)
            camera.y = OrgPos.y;
    }
    public void Shake(int _CameraID)
    {
        shakeInfo.StartDelay = 0.7f;
        shakeInfo.TotalTime = 3f;
        shakeInfo.UseTotalTime = true;

        shakeInfo.Shake = new Vector3(0.3f, 0.3f, 0f);

        shakeInfo.Dest = shakeInfo.Shake;
        shakeInfo.Dir = shakeInfo.Shake;
        shakeInfo.Dir.Normalize();

        shakeInfo.RemainDist = shakeInfo.Shake.magnitude;
        shakeInfo.RemainCountDis = float.MaxValue;

        shakeInfo.Veclocity = 8;

        shakeInfo.Damping = 0.5f;
        shakeInfo.UseDamping = true;

        shakeInfo.DampingTine = shakeInfo.RemainDist / shakeInfo.Veclocity;

        shakeInfo.Count = 4;
        shakeInfo.UseCount = true;

        StopCoroutine("ShakeCoroutine");
        ResetShakeTr();
        StartCoroutine("ShakeCoroutine");
    }

    IEnumerator ShakeCoroutine()
    {
        CameraShake = true;

        float dt, dist;
        if (shakeInfo.StartDelay > 0)
            yield return new WaitForSeconds(shakeInfo.StartDelay);
        while (true)
        {
            dt = Time.fixedDeltaTime;
            dist = dt * shakeInfo.Veclocity;

            if ((shakeInfo.RemainDist -= dist) > 0)
            {
                ShakeTr.localPosition += shakeInfo.Dir * dist;

                //float rc = transform.position.x - FovX - Left;

                //if (rc < 0)
                //    ShakeTr.localPosition += new Vector3(-rc, 0, 0);

                //rc = Right - (transform.position.x + FovX);

                //if (rc < 0)
                //    ShakeTr.localPosition += new Vector3(rc, 0, 0);

                CameraLimit(true);

                if (shakeInfo.UseCount)
                {
                    if ((shakeInfo.RemainCountDis -= dist) < 0)
                    {
                        shakeInfo.RemainCountDis = float.MaxValue;

                        if (--shakeInfo.Count < 0)
                            break;
                    }
                }
            }
            else
            {
                if (shakeInfo.UseDamping)
                {
                    float distdamping = Mathf.Max(
                        shakeInfo.Damping * shakeInfo.DampingTine,
                        shakeInfo.Damping * dt);

                    if (shakeInfo.Shake.magnitude > distdamping)
                        shakeInfo.Shake -= shakeInfo.Dir * distdamping;
                    else
                    {
                        shakeInfo.UseCount = true;
                        shakeInfo.Count = 1;
                    }
                }
                ShakeTr.localPosition = shakeInfo.Dest - shakeInfo.Dir *
                    (-shakeInfo.RemainDist);

                //float rc = transform.position.x - FovX - Left;

                //if (rc < 0)
                //    ShakeTr.localPosition += new Vector3(-rc, 0, 0);

                //rc = Right - (transform.position.x + FovX);

                //if (rc < 0)
                //    ShakeTr.localPosition += new Vector3(rc, 0, 0);

                CameraLimit(true);

                shakeInfo.Shake = -shakeInfo.Shake;
                shakeInfo.Dest = shakeInfo.Shake;
                shakeInfo.Dir = -shakeInfo.Dir;

                float len = shakeInfo.Shake.magnitude;

                shakeInfo.RemainCountDis = len + shakeInfo.RemainDist;
                shakeInfo.RemainDist += len * 2f;

                shakeInfo.DampingTine = shakeInfo.RemainDist / shakeInfo.Veclocity;

                if (shakeInfo.RemainDist < dist)
                    break;
            }

            if (shakeInfo.UseTotalTime && (shakeInfo.TotalTime -= dt) < 0)
                break;


            yield return new WaitForFixedUpdate();
        }

        ResetShakeTr();

        yield break;
    }
}
