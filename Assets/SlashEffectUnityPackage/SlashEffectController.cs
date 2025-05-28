
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SlashEffectController : MonoBehaviour
{
    public Transform swordBase;
    public Transform swordTip;
    public float duration = 0.4f;
    public int maxTrailPoints = 20;

    private List<Vector3> basePoints = new List<Vector3>();
    private List<Vector3> tipPoints = new List<Vector3>();
    private Mesh mesh;
    private float elapsed;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        ClearTrail();
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed < duration)
        {
            AddTrailPoint();
            UpdateMesh();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void AddTrailPoint()
    {
        if (basePoints.Count >= maxTrailPoints)
        {
            basePoints.RemoveAt(0);
            tipPoints.RemoveAt(0);
        }

        basePoints.Add(swordBase.position);
        tipPoints.Add(swordTip.position);
    }

    void ClearTrail()
    {
        basePoints.Clear();
        tipPoints.Clear();
        mesh.Clear();
    }

    void UpdateMesh()
    {
        if (basePoints.Count < 2) return;

        int count = basePoints.Count;
        Vector3[] vertices = new Vector3[count * 2];
        Vector2[] uvs = new Vector2[count * 2];
        int[] triangles = new int[(count - 1) * 6];

        for (int i = 0; i < count; i++)
        {
            vertices[i * 2] = transform.InverseTransformPoint(basePoints[i]);
            vertices[i * 2 + 1] = transform.InverseTransformPoint(tipPoints[i]);

            float t = (float)i / (count - 1);
            uvs[i * 2] = new Vector2(t, 0);
            uvs[i * 2 + 1] = new Vector2(t, 1);
        }

        for (int i = 0; i < count - 1; i++)
        {
            int idx = i * 6;
            int vi = i * 2;

            triangles[idx] = vi;
            triangles[idx + 1] = vi + 1;
            triangles[idx + 2] = vi + 2;

            triangles[idx + 3] = vi + 2;
            triangles[idx + 4] = vi + 1;
            triangles[idx + 5] = vi + 3;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }
}
