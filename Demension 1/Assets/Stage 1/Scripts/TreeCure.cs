using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCure : MonoBehaviour {

    private Slider toilet;
    public GameObject TreeObject;
    public GameObject TO;
    public GameObject TO1;
    public GameObject TO2;
    private AudioSource audio;
    public AudioClip Effect_Cure;


    void Awake()
    {
        toilet = GameObject.Find("toilet").GetComponent<Slider>();
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.Effect_Cure;
        this.audio.loop = false;

    }

    void Start()
    {
        TO.SetActive(false);
        TO1.SetActive(false);
        TO2.SetActive(false);
    }

    void Update()
    {
        if(toilet.value >= 0.3f)
        {
            TO.SetActive(true);
            if(toilet.value >= 0.6f)
            {
                TO1.SetActive(true);
                if (toilet.value >= 0.9f)
                {
                    TO2.SetActive(true);
                }
            }
        }
    }
}
