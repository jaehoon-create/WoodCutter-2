using System.Collections;
using UnityEngine;

public class GPSManager : MonoBehaviour
{

    public static float magneticHeading;
    public static float trueHeading;

    private void Awake()
    {
        Input.location.Start(); //위치 서비스 시작
        Input.compass.enabled = true; //나침반 활성화
    }

    IEnumerator Start()
    {
        while (true)
        {

            //헤딩 값 가져오기
            if (Input.compass.headingAccuracy == 0 || Input.compass.headingAccuracy > 0)
            {
                magneticHeading = Input.compass.magneticHeading;
                trueHeading = Input.compass.trueHeading;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
