using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isHard = false;
    public bool isHardPossible = false;

    public int leftCards = 0;

    public Card firstCard = null;
    public Card secondCard = null;

    public Text timeTxt;
    public Transform MainCanvas;
    public GameObject prefabsFailPanel;

    float time = 30f;

    bool isTimeoutWarning = false;
    bool isFail = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� �� �������� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� GameManager ����
        }
    }
    void Update()
    {
        if (leftCards > 0 && timeTxt != null && !isFail)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
        if(time <= 10f && !isTimeoutWarning)
        {
            Warning();
        }
        if(time <= 0f && !isFail)
        {
            isFail = true;
            SoundManager.instance.StopBGMWithFadeOut(2f, 0);
            SoundManager.instance.StopWarningBGM();
            SoundManager.instance.PlaySFX("fail");
            time = 0f;
            timeTxt.text = "0.00";
            Instantiate(prefabsFailPanel, MainCanvas);
        }
    }

    public void Init()
    {
        isFail = false;
        isTimeoutWarning = false;
        time = 30.0f;
        Time.timeScale = 1.0f;
    }

    void Warning()
    {
        isTimeoutWarning = true;
        SoundManager.instance.StopBGMWithFadeOut(2f, 0.3f);
        SoundManager.instance.PlayWarningBGMWithFadeIn("timeoutWarning", 1f);
    }

    public void Change_Level()
    {
        isHard = !isHard;
    }

    // firstCard�� secondCard ����(idx) ���ϱ�
    public void MatchCards()
    {
        // idx �����ϸ�,
        if (firstCard.idx == secondCard.idx)
        {
            SoundManager.instance.PlaySFX("Correct");

            // ī�� greyscale ��ȯ
            firstCard.GreyCard();
            secondCard.GreyCard();

            // leftCards ����
            leftCards -= 2;

            // leftCards == 0�� ��,
            if (leftCards <= 0)
            {
                // clear sound
                SoundManager.instance.StopBGMWithFadeOut(4f, 0);
                SoundManager.instance.StopWarningBGM();
                SoundManager.instance.PlaySFX("Clear");

                // ��������
                // EndScene ��ȯ
                if (isHard == false && isHardPossible == false)
                {
                    // isHardPossible �� ��ȯ
                    isHardPossible = true;
                }
                Invoke("NextToEndScene", 1.0f);
                // �������?Ŭ���� ��, �ϵ��� �ر�
            }
        }

        // idx �������� ���� ��,
        else
        {
            SoundManager.instance.PlaySFX("Incorrect");

            // ī�� �ݱ�
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        // ���� �ʱ�ȭ
        firstCard = null;
        secondCard = null;
    }

    public void NextToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
}
