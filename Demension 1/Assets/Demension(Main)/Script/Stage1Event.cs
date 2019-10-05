using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Event : MonoBehaviour {
    
    public GameObject Player;

    public GameObject[] ImageArray = new GameObject[8];
    public GameObject GuideImage;

    private int ImageArrayNum = 0;

    float T_time = 0;
   
	// Use this for initialization
	void Start () {
        Player.GetComponent<Player>().enabled = false;
        SetOffImage();

        ImageArray[0].SetActive(true);
        GuideImage.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        Guide_Text();
        
    }
    public void Guide_Text()
    {
        T_time = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ImageArrayNum < ImageArray.Length-1)
            {
                ImageArrayNum++;
                SetOffImage();
                ImageArray[ImageArrayNum].SetActive(true);
                GuideImage.SetActive(true);
            }
            else if(ImageArrayNum >= ImageArray.Length-1)
            {
                ImageArray[ImageArrayNum].SetActive(false);
                GuideImage.SetActive(false);
            }
            Player.GetComponent<Player>().enabled = true;
            
        }                   
    }

    private void SetOffImage()
    {
        for (int i = 0; i < ImageArray.Length; i++)
        {
            ImageArray[i].SetActive(false);
            GuideImage.SetActive(false);
        }
    }
}
