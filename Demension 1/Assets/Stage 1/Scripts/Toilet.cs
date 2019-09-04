using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Toilet : MonoBehaviour
{
    public GameObject Result;
    public Slider toilet;
    private bool Toilet_Max = false;
    public float step_timer = 0;
    public GUIStyle guistyle;
    private AudioSource audio;
    public AudioClip jumpSound;
    

    // Use this for initialization
    void Start()
    {
        this.guistyle.fontSize = 64;
        toilet.value = 0.0f;
        Result.gameObject.SetActive(false);
        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.jumpSound;
        this.audio.loop = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(TimeControl.instance.GameActive)
        {
            this.step_timer += Time.deltaTime;

            Result_screen();
            if (toilet.value == 1f)
            {
                Toilet_Max = true;
            }
            if (Toilet_Max == false)
            {
                Time.timeScale = 1;
            }
        }
    }

    public void Result_screen()
    {
        if (Toilet_Max == true)
        {
            Result.SetActive(true);
            TimeControl.instance.GameActive = false;
            audio.PlayOneShot(jumpSound);
        }
        
    }
    void OnGUI()
    {
        GUI.color = Color.black;
        if(Toilet_Max == true)
        {
            Time.timeScale = 0;
            GUI.Label(new Rect(300, 150, 200, 20), step_timer.ToString("0.00"), guistyle);
        }

    }
    public void Click()
    {
        SceneManager.LoadScene(1);
        Toilet_Max = false;
    }
}