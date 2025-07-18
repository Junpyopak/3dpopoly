using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Frost")]
public class FrostEffect : MonoBehaviour
{
    public float FrostAmount = 0.5f; //0-1 (0=minimum Frost, 1=maximum frost)
    public float EdgeSharpness = 1; //>=1
    public float minFrost = 0; //0-1
    public float maxFrost = 1; //0-1
    public float seethroughness = 0.2f; //blends between 2 ways of applying the frost effect: 0=normal blend mode, 1="overlay" blend mode
    public float distortion = 0.1f; //how much the original image is distorted through the frost (value depends on normal map)
    public Texture2D Frost; //RGBA
    public Texture2D FrostNormals; //normalmap
    public Shader Shader; //ImageBlendEffect.shader
	
	private Material material;
    private Coroutine frostTime;
    private void Awake()
	{
        if (Shader == null)
        {
            Shader = Shader.Find("Custom/ImageBlendEffect");
            if (Shader == null)
            {
                Debug.LogError(" FrostEffect: 'Custom/ImageBlendEffect' ���̴��� ã�� �� �����ϴ�!");
                enabled = false;
                return;
            }
        }
        material = new Material(Shader);
        //material.SetTexture("_BlendTex", Frost);
        //material.SetTexture("_BumpMap", FrostNormals);
        if (Frost != null)
            material.SetTexture("_BlendTex", Frost);

        if (FrostNormals != null)
            material.SetTexture("_BumpMap", FrostNormals);
    }
	
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!Application.isPlaying)
        {
            material.SetTexture("_BlendTex", Frost);
            material.SetTexture("_BumpMap", FrostNormals);
            EdgeSharpness = Mathf.Max(1, EdgeSharpness);
        }
        material.SetFloat("_BlendAmount", Mathf.Clamp01(Mathf.Clamp01(FrostAmount) * (maxFrost - minFrost) + minFrost));
        material.SetFloat("_EdgeSharpness", EdgeSharpness);
        material.SetFloat("_SeeThroughness", seethroughness);
        material.SetFloat("_Distortion", distortion);
        Debug.Log("_Distortion: "+ distortion);

		Graphics.Blit(source, destination, material);
	}

    public void PlayFrostEffect(float targetAmount = 0.31f, float fadeTime = 0.5f, float holdTime = 1.0f)
    {
        if (frostTime != null)
            StopCoroutine(frostTime);

        frostTime = StartCoroutine(FrostRoutine(targetAmount, fadeTime, holdTime));
    }

    private IEnumerator FrostRoutine(float targetAmount, float fadeTime, float holdTime)
    {
        float timer = 0f;

        // ��� ����
        while (timer < fadeTime)
        {
            FrostAmount = Mathf.Lerp(0f, targetAmount, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        FrostAmount = targetAmount;

        // ���� ����
        yield return new WaitForSeconds(holdTime);

        // ���� ����
        timer = 0f;
        while (timer < fadeTime)
        {
            FrostAmount = Mathf.Lerp(targetAmount, 0f, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        FrostAmount = 0f;

        frostTime = null;
    }
}