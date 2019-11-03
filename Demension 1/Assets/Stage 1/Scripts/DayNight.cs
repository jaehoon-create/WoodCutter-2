using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecound;
    private bool isNight = false;
    [SerializeField] private float fogDensityCalc;// 증감량비율
    [SerializeField] private float nightFogDensity;//밤상태의 Fog비율 밀도
    private float dayFogDensity;//낮상태의 fog 밀도
    private float currentFogDensity; // 계산​
    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * Time.deltaTime);
        if (transform.eulerAngles.x >= 170)
            isNight = true;
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
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
                RenderSettings.fogDensity = currentFogDensity;
            }
        }
    }
}