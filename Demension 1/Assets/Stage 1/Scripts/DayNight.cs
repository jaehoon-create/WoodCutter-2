using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNight : MonoBehaviour
{
    // 시계 슬라이더
    public Image Daynight_time;
    private float itemcooldownTime;
    private float updateTime;
    private bool canSlider;
    public float Timer;

    [SerializeField] private float secondPerRealTimeSecound;
    private bool isNight = false;
    [SerializeField] private float fogDensityCalc;// 증감량비율
    [SerializeField] private float nightFogDensity;//밤상태의 Fog비율 밀도
    private float dayFogDensity;//낮상태의 fog 밀도
    private float currentFogDensity; // 계산​

    void Start()
    {
        
        canSlider = true;
        updateTime = 0.0f;
        itemcooldownTime = Timer; //초단위의 시간 
        dayFogDensity = RenderSettings.fogDensity;
    }
    // Update is called once per frame
    void Update()
    {
        if (canSlider){
            updateTime = updateTime + Time.deltaTime;
            Daynight_time.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, updateTime / itemcooldownTime) / 100);
            
            if(updateTime > itemcooldownTime)
            {
                updateTime = 0.0f;
                itemcooldownTime = Timer;
                Daynight_time.fillAmount = 1.0f;
            }
        }

        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * Time.deltaTime);
        if (transform.eulerAngles.x >= 170)
        {   isNight = true;
        }
        else if (transform.eulerAngles.x <= 10)
            isNight = false;

        if (isNight)
        {
            if (currentFogDensity <= nightFogDensity)
            {
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
            {
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
        
    }
}