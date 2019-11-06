using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Event : MonoBehaviour {
    
    public GameObject Player;
    public GameObject[] ImageArray = new GameObject[8];
    public GameObject GuideImage;
    public bool Time_set;

    private int ImageArrayNum = 0;

    float T_time = 0;
   
	// Use this for initialization
	void Start () {
        Player.GetComponent<Player>().enabled = false;
        SetOffImage();
        Time_set = false;
        ImageArray[0].SetActive(true);
        GuideImage.SetActive(true);
    }

    // Update is called once per frame
    void Update() {

        if (Time_set == false)
        {
            Guide_Text();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Player.GetComponent<Player>().enabled = true;
            ImageArray[6].SetActive(false);
            ImageArray[7].SetActive(false);
            GuideImage.SetActive(false);
        }
    }

    public void Guide_Text()
    {
     //   T_time = Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ImageArrayNum < 5)
            {
                ImageArrayNum++;
                SetOffImage();
                ImageArray[ImageArrayNum].SetActive(true);
                GuideImage.SetActive(true);
            }
            else if(ImageArrayNum >=5)
            {
                    ImageArray[ImageArrayNum].SetActive(false);
                    GuideImage.SetActive(false);
                    Player.GetComponent<Player>().enabled = true;
                    Time_set = true;
            }
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Way1"))
        {
            Player.GetComponent<Player>().enabled = false;
            ImageArray[6].SetActive(true);
            GuideImage.SetActive(true);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Way2"))
        {
            Player.GetComponent<Player>().enabled = false;
            ImageArray[7].SetActive(true);
            GuideImage.SetActive(true);
        }
    }
}
