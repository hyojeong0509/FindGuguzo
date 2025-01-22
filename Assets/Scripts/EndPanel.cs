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

    void Start()
    {
        // 하드모드를 클리어 하였는지 GameManager에서 isHard를 가져외서 체크
        bool isHardMode = GameManager.Instance.isHard;
        // 하드모드를 클리어 한 경우면 HardPanel을 보여주고 EazyPanel은 숨김
        if (isHardMode)
        {
            HardPanel.SetActive(true);
            EazyPanel.SetActive(false);
        }
        // 하드모드를 클리어 한 경우가 아니라면 그냥 EazyPanel을 보여줌.
        else
        {
            HardPanel.SetActive(false);
            EazyPanel.SetActive(true);
        }
    }

    // 이미지를 클릭했을 때 닫는 버튼
    public void CloseBtn()
    {
        ImgPanel.SetActive(false);
    }


}
