using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    float grey = 64f / 255f;

    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public SpriteRenderer frontImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 카드 열기
    public void OpenCard()
    {
        // secondCard에 할당된 정보가 있다면 작동하지 않기
        if (GameManager.Instance.secondCard != null) { return; }

        // 카드의 앞면 보이기
        front.SetActive(true);
        back.SetActive(false);

        // firstCard에 할당된 정보가 없다면,
        if (GameManager.Instance.firstCard == null)
        {
            // firstCard에 정보 할당하기
            GameManager.Instance.firstCard = this;
        }

        // firstCard에 할당된 정보가 있다면,
        else
        {
            // secondCard에 정보 할당하기
            GameManager.Instance.secondCard = this;
            // first - second의 정보(idx) 비교하기
            GameManager.Instance.MatchCards();
        }
    }

    // 카드 닫기
    public void CloseCard()
    {
        Invoke("CloseCardRaw", 1.0f);
    }

    void CloseCardRaw()
    {
        front.SetActive(false);
        back.SetActive(true);
    }

    // 카드 greyscale 변환하기
    public void GreyCard()
    {
        Invoke("GreyCardRaw", 1.0f);
    }
    
    void GreyCardRaw()
    {
        frontImage.color = new Color(grey, grey, grey, 1.0f);
    }
}
