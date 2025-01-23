using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    float grey = 63f / 255f;

    public int idx = 0;

    public SpriteRenderer frontImage;
    public SpriteRenderer backImage;

    public GameObject front;
    public GameObject back;
    public GameObject backBtn;
    public Animator anim;
    // Update is called once per frame
    public void Update()
    {
        if(GameManager.Instance.isFail == true)
        {
            backBtn.SetActive(false);
        }
    }
    public void Setting(int number, int backNumber)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Gugu{idx}");
        backImage.sprite = Resources.Load<Sprite>($"Cat{backNumber}");
    }

    // 카드 열기
    public void OpenCard()
    {
        SoundManager.instance.PlaySFX("CardFlip");
        // Debug.Log("card selected");
        // secondCard에 할당된 정보가 있다면 작동하지 않기
        if (GameManager.Instance.secondCard != null) 
        {
            return;
        }

        // 카드의 앞면 보이기
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);
        // disactive card button
        // and should re-activate card button on close method

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
        anim.SetBool("isOpen", false);
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
