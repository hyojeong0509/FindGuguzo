using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject easyBG;
    public GameObject hardBG;
    public GameObject easyTimeBox;
    public GameObject hardTimeBox;

    /*private void Awake()
    {
        if (InstanceBG == null)
        {
            InstanceBG = this;
            DontDestroyOnLoad(gameObject);  // not to destroy BackgroundManager when scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        // Set Background objects according to difficulty
        // if isHard == false;
        if (!GameManager.Instance.isHard)
        {
            // set easy background & easy UI box
            easyBG.SetActive(true);
            easyTimeBox.SetActive(true);
            // turn off hard background & hard UI box
            hardBG.SetActive(false);
            hardTimeBox.SetActive(false);
        }
        else
        {
            easyBG.SetActive(false);
            easyTimeBox.SetActive(false);
            hardBG.SetActive(true);
            hardTimeBox.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
