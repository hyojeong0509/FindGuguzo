using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextOBJ : MonoBehaviour
{
    [SerializeField] Text timeText;


    void Start()
    {
        GameManager.Instance.timeTxt = timeText;
    }
}