using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isHard = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 시 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 GameManager 제거
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
