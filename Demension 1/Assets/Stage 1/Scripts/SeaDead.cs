using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeaDead : MonoBehaviour {

    private Slider gage;

    // Use this for initialization
    void Start()
    {
        gage = GameObject.Find("uesr stat").GetComponent<Slider>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gage.value > 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
