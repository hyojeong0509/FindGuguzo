using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public GameObject HardPanel;
    public GameObject EazyPanel;
    public GameObject ImgPanel;

    public void GameOver()
    {
        bool isHardMode = GameManager.Instance.isHard;
        if (isHardMode)
        {
            HardPanel.SetActive(true);
            EazyPanel.SetActive(false);
        }
        else
        {
            HardPanel.SetActive(false);
            EazyPanel.SetActive(true);
        }
    }


    public void CloseBtn()
    {
        ImgPanel.SetActive(false);
    }


}
