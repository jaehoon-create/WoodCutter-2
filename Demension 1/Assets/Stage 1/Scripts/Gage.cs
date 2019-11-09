using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Gage : MonoBehaviour
{
    public Slider gage;
    public Slider toilet;
    public GameObject fail;
    public GameObject AudioSource;
    private bool Gage_button = false;
    private AudioSource audio;
    public AudioClip Back_Sound;
    public AudioClip Result_s;


    // Use this for initialization
    void Start()
    {
        gage.value = 1.0f;
        fail.gameObject.SetActive(false);
        this.audio = this.gameObject.AddComponent<AudioSource>();
        toilet = GameObject.Find("toilet").GetComponent<Slider>();
        this.audio.clip = this.Back_Sound;
        this.audio.loop = false;
        this.audio.Play();
        audio.volume = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeControl.instance.GameActive)
        {
            gage.value -= Time.deltaTime * 0.02f;

            
            
            if (gage.value == 0)
            {
                audio.Pause();
                fail.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            if (toilet.value == 1.0f)
            {
                audio.Pause();
            }
        }
    }
   
}
