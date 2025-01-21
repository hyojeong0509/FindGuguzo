using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int idx = 0;
    public SpriteRenderer frontImage;
    public SpriteRenderer backImage;



    // Update is called once per frame
    public void Setting(int number, int backNumber)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"Gugu{idx}");
        backImage.sprite = Resources.Load<Sprite>($"Cat{backNumber}");
    }
}
