using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;//배경음
    public AudioSource sfxSource; //효과
    public AudioSource loopSource;

    [Header("BGM Clips")]
    public AudioClip walkBGM;
    public AudioClip battleBGM;

    [Header("SFX Clips")]
    public AudioClip footstepClips; // 발소리
    public AudioClip hitClip;         //피격음
    public AudioClip attackClip;//플레이어 공격음
    public AudioClip RunClip;
    public AudioClip BossDeathClip;
    public AudioClip BossRoarClip;
    public AudioClip LevupClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //BGM플레이
    public void PlayBGM(string type)
    {
        AudioClip clip = null;

        switch (type)
        {
            case "Walk": clip = walkBGM; break;
            case "Battle": clip = battleBGM; break;
        }

        if (clip == null || bgmSource.clip == clip) return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // SFX플레이
    public void PlaySFX(string type, float volume = 1f)
    {
        AudioClip clip = null;

        switch (type)
        {
            case "Footstep":
                clip = footstepClips;
                break;

            case "Hit":
                clip = hitClip;
                break;

            case "AttackSound":
                clip = attackClip;
                break;
            case "RunStep":
                clip = RunClip;
                break;
            case "BossRoar":
                clip = BossRoarClip;
                break;
            case "BossDeath":
                clip = BossDeathClip;
                break;
            case "Levelup":
                clip = LevupClip;
                break;
        }

        if (clip != null)
            sfxSource.PlayOneShot(clip, volume);
    }
    public void PlayLoop(string type)
    {
        switch (type)
        {
            case "Footstep":
                loopSource.clip = footstepClips;
                break;
            case "RunStep":
                loopSource.clip = RunClip;
                break;
        }
        if (loopSource.clip != null && !loopSource.isPlaying)
        {
            loopSource.loop = true;
            loopSource.Play();
        }
    }

    public void StopLoop()
    {
        if (loopSource.isPlaying)
            loopSource.Stop();
    }
}
