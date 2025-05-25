using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMotion : MonoBehaviour
{
    public static SlashMotion instance;
    [SerializeField] private Transform swordTip;

    [SerializeField] private GameObject slashEffectPrefab;

    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private float duration = 0.3f;

    private List<Vector3> trailPositions = new List<Vector3>();
    private bool isRecording = false;
    private float timeSinceLast = 0f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void LateUpdate()
    {
        if (!isRecording) return;

        timeSinceLast += Time.deltaTime;
        if (timeSinceLast >= spacing)
        {
            trailPositions.Add(swordTip.position);
            timeSinceLast = 0f;
        }
    }

    public void StartRecordingTrail()
    {
        trailPositions.Clear();
        isRecording = true;
        timeSinceLast = 0f;
    }

    public void StopRecordingTrail()
    {
        isRecording = false;
        SpawnTrailEffects();
    }

    private void SpawnTrailEffects()
    {
        for (int i = 1; i < trailPositions.Count; i++)
        {
            Vector3 from = trailPositions[i - 1];
            Vector3 to = trailPositions[i];
            Vector3 mid = (from + to) / 2f;
            Vector3 dir = to - from;

            float length = dir.magnitude;
            if (length < 0.01f) continue;

            Quaternion rotation = Quaternion.LookRotation(dir);

            GameObject effect = Instantiate(slashEffectPrefab, mid, rotation);
            effect.transform.localScale = new Vector3(length, 1f, 1f);

            Destroy(effect, duration);
        }
    }
}
