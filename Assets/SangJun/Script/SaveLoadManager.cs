using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    [SerializeField] StartSceneManager startSceneManager;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
            LoadData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("isHardPossible")) // 키가 존재하는 경우
        {
            // int 값을 bool로 변환
            GameManager.Instance.isHardPossible = PlayerPrefs.GetInt("isHardPossible") == 1;
        }

        /*if (PlayerPrefs.HasKey("BGMVolume")) // 키가 존재하는 경우
        {
            SoundManager.instance.BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume")) // 키가 존재하는 경우
        {
            SoundManager.instance.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        }
        startSceneManager.soundVisualInit();*/
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("isHardPossible", GameManager.Instance.isHardPossible ? 1 : 0); // bool 값을 int로 변환
       // PlayerPrefs.SetFloat("BGMVolume", SoundManager.instance.BGMVolume);
       // PlayerPrefs.SetFloat("SFXVolume", SoundManager.instance.SFXVolume);
        PlayerPrefs.Save(); // 변경사항 저장
        Debug.Log("저장");
    }
}
