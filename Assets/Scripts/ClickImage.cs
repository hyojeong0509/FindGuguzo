using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickImage : MonoBehaviour
{
    public GameObject ImgPanel;
    public Image imgDisplay;

    // 이미지를 클릭하면 클릭한 이미지를 가져와서 큰 이미지로 적용
    public void ViewImage(Image clickImage)
    {
        imgDisplay.sprite = clickImage.sprite;
        ImgPanel.SetActive(true);
    }
}
