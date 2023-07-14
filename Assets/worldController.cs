using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  

public class worldController : MonoBehaviour
{
    public GameObject HeadPos;
    public GameObject OVRrig;

    public GameObject OVRCam;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(HeadPos.transform.position-OVRCam.transform.position);
        OVRrig.transform.position += HeadPos.transform.position-OVRCam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var keyb = Keyboard.current;
        if(keyb.spaceKey.wasPressedThisFrame){
            Debug.Log(HeadPos.transform.position-OVRCam.transform.position);
            OVRrig.transform.position += HeadPos.transform.position-OVRCam.transform.position;
        }
    }
}
