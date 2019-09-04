using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Monstercol : MonoBehaviour {
    private AudioSource audio;
    public AudioClip jumpSound;
    private Slider gage;

	// Use this for initialization
	void Start () {
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.jumpSound;
        this.audio.loop = false;
        gage = GameObject.Find("uesr stat").GetComponent<Slider>();
        Destroy(gameObject, 13f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gage.value > 0)
        {
            gage.value -= 0.1f;
        }

        this.audio.Play();
    }
    // Update is called once per frame
    void Update () {
	}
}
