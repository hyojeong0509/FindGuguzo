using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneBackground : MonoBehaviour
{
    public GameObject easyFloor;
    public GameObject hardFloor;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.isHard == false)
        {
            easyFloor.SetActive(true);
            hardFloor.SetActive(false);
        }
        else
        {
            easyFloor.SetActive(false);
            hardFloor.SetActive(true);
        }
    }
}
