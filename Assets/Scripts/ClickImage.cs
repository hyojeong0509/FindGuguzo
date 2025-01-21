using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickImage : MonoBehaviour
{
    public GameObject ImgPanel;
    public Image imgDisplay;

    public void ViewImage(Image clickImage)
    {
        imgDisplay.sprite = clickImage.sprite;
        ImgPanel.SetActive(true);
    }
}
