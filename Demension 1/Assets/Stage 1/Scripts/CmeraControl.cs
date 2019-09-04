using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmeraControl : MonoBehaviour {

    public GameObject player;
    public float offsetX = 0f;
    public float offsetY = 10f;
    public float offsetZ = -15f;

    public float followspeed = 3f;

    Vector3 cameraposition;

    void LateUpdate()
    {
        cameraposition.x = player.transform.position.x + offsetX;
        cameraposition.y = player.transform.position.y + offsetY;
        cameraposition.z = player.transform.position.z + offsetZ;

        //cameraposition.y = player.transform.rotation.y + offsetY;

        transform.position = Vector3.Lerp(transform.position, cameraposition, followspeed * Time.deltaTime);
    }
}
