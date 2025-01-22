using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private Dictionary<string, AudioClip> soundDict;  // SFX와 BGM을 저장할 Dictionary
    [SerializeField] private AudioSource sfxPlayer;                   // SFX 재생용 AudioSource
    [SerializeField] private AudioSource bgmPlayer;                   // BGM 재생용 AudioSource
    [SerializeField] private AudioSource warningBgmPlayer;                  

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] audioClips; // 오디오 클립 배열

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySFX("Click");
        }
    }

    private void Init()
    {
        soundDict = new Dictionary<string, AudioClip>();
        bgmPlayer.loop = true; // BGM은 기본적으로 반복 재생
        bgmPlayer.volume = 0.15f;

        // Dictionary 초기화
        foreach (var clip in audioClips)
        {
            soundDict[clip.name] = clip;
        }
    }

    // SFX 재생
    public void PlaySFX(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            sfxPlayer.volume = 0.5f;
            if (soundName == "CardFlip")
            {
                sfxPlayer.volume = 0.8f;
            }
            sfxPlayer.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found.");
        }
    }

    // BGM 재생
    public void PlayBGM(string bgmName)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            bgmPlayer.clip = clip;
            bgmPlayer.Play();
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }
    public void PlayBGMWithFadeIn(string bgmName, float fadeDuration)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            bgmPlayer.clip = clip;
            bgmPlayer.volume = 0.05f; // 볼륨을 0으로 설정
            bgmPlayer.Play();
            StartCoroutine(FadeInBGM(fadeDuration));
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }
    // 페이드인 효과를 구현하는 코루틴
    private IEnumerator FadeInBGM(float duration)
    {
        float targetVolume = 0.15f; // 페이드인의 최종 볼륨 (bgmPlayer 기본 볼륨)
        float currentVolume = 0f;

        while (currentVolume < targetVolume)
        {
            currentVolume += Time.deltaTime / duration; // 점진적으로 볼륨 증가
            bgmPlayer.volume = currentVolume;
            yield return null;
        }

        bgmPlayer.volume = targetVolume; // 최종 볼륨으로 설정
    }
    public void StopBGMWithFadeOut(float fadeDuration, float _targetVolume)
    {
        if (bgmPlayer.isPlaying)
        {
            StartCoroutine(FadeOutBGM(fadeDuration, _targetVolume));
        }
    }
    // 페이드아웃 효과를 구현하는 코루틴
    private IEnumerator FadeOutBGM(float duration, float _targetVolume)
    {
        float targetVolume = bgmPlayer.volume * _targetVolume; // 페이드인의 최종 볼륨 (bgmPlayer 기본 볼륨)
        float currentVolume = bgmPlayer.volume;

        while (currentVolume > targetVolume)
        {
            currentVolume -= Time.deltaTime / duration; // 점진적으로 볼륨 하락
            bgmPlayer.volume = currentVolume;
            yield return null;
        }

        bgmPlayer.volume = targetVolume; // 최종 볼륨으로 설정
    }

    public void AddPlayBGM(string bgmName, float fadeDuration)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            if (bgmPlayer.clip != clip)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.volume = 0.05f; // 볼륨을 0으로 설정
                bgmPlayer.Play();
                StartCoroutine(FadeInBGM(fadeDuration));
            }
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }



    //
    // 페이드인 효과를 구현하는 코루틴 (경고음 전용)
    public void PlayWarningBGMWithFadeIn(string bgmName, float fadeDuration)
    {
        if (soundDict.TryGetValue(bgmName, out var clip))
        {
            warningBgmPlayer.clip = clip;
            warningBgmPlayer.loop = true;
            warningBgmPlayer.volume = 0.05f; // 볼륨을 0으로 설정
            warningBgmPlayer.Play();
            StartCoroutine(FadeInWarningBGM(fadeDuration));
        }
        else
        {
            Debug.LogWarning("BGM not found.");
        }
    }
    private IEnumerator FadeInWarningBGM(float duration)
    {
        float targetVolume = 0.5f; // 페이드인의 최종 볼륨 (bgmPlayer 기본 볼륨)
        float currentVolume = 0f;

        while (currentVolume < targetVolume)
        {
            currentVolume += Time.deltaTime / duration; // 점진적으로 볼륨 증가
            warningBgmPlayer.volume = currentVolume;
            yield return null;
        }

        warningBgmPlayer.volume = targetVolume; // 최종 볼륨으로 설정
    }
    public void StopWarningBGM()
    {
        if (warningBgmPlayer.isPlaying)
        {
            warningBgmPlayer.Stop();
        }
    }
}