using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour {
    private AudioSource audio;
    public AudioClip jumpSound;
    
    // Use this for initialization
    void Start () {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.jumpSound;
        this.audio.loop = false;
    }
    
    public void Auudio()
    {
        audio.Play();
    }

	// Update is called once per frame
	void Update () {

    }

    public void Click_Start()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("Button CLick");
    }
}

