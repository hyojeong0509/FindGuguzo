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

    float time = 30f;

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

    void Start()
    {
        time = 30.0f;
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (leftCards > 0 && timeTxt != null)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("N2");
        }
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
            // ī�� greyscale ��ȯ
            firstCard.GreyCard();
            secondCard.GreyCard();

            // leftCards ����
            leftCards -= 2;

            // leftCards == 0�� ��,
            if (leftCards <= 0)
            {
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
