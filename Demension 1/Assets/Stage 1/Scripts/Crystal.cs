using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour {

    private Slider toilet;
    public int PlantGage;
    private AudioSource audio;
    public AudioClip Effect1;
    public GameObject effect_hit;


    void Start () {
        toilet = GameObject.Find("toilet").GetComponent<Slider>();
        PlantGage = 2;
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.Effect1;
        audio.volume = 0.1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audio.Play();

        }
        toilet.value -= Time.deltaTime * 0.01f;
        
    }
}
