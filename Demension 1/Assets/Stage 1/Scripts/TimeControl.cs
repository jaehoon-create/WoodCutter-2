using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour {


    public static TimeControl instance = null;

    public bool GameActive;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {

        //if (Input.GetKeyDown(KeyCode.P))
        //{

        //    if (GameActive == true)
        //        GameActive = false;
        //    else
        //        GameActive = true;
        //}
    }
}
