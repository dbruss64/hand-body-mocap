using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCopy : MonoBehaviour
{
    public enum HandSide{
        Left,
        Right
    }

    public HandSide CHandSide = HandSide.Right;

    public Vector3 rotation1;

    public GameObject OVRHandPrefab;

    private GameObject OVRHandRoot;
    public GameObject AvatarHandRoot;

    private Dictionary<string, string> jointMap = new Dictionary<string, string>(){
        {"Hand_Thumb0",                                     "<Side>HandThumb1"},
        {"Hand_Thumb0/Hand_Thumb1",                         "<Side>HandThumb1/<Side>HandThumb2"},
        {"Hand_Thumb0/Hand_Thumb1/Hand_Thumb2",             "<Side>HandThumb1/<Side>HandThumb2/<Side>HandThumb3"},
        {"Hand_Thumb0/Hand_Thumb1/Hand_Thumb2/Hand_Thumb3", "<Side>HandThumb1/<Side>HandThumb2/<Side>HandThumb3/<Side>HandThumb4"},
        {"Hand_Index1",                         "<Side>HandIndex1"},
        {"Hand_Index1/Hand_Index2",             "<Side>HandIndex1/<Side>HandIndex2"},
        {"Hand_Index1/Hand_Index2/Hand_Index3", "<Side>HandIndex1/<Side>HandIndex2/<Side>HandIndex3"},
        {"Hand_Middle1",                            "<Side>HandMiddle1"},
        {"Hand_Middle1/Hand_Middle2",               "<Side>HandMiddle1/<Side>HandMiddle2"},
        {"Hand_Middle1/Hand_Middle2/Hand_Middle3",  "<Side>HandMiddle1/<Side>HandMiddle2/<Side>HandMiddle3"},
        {"Hand_Ring1","<Side>HandRing1"},
        {"Hand_Ring1/Hand_Ring2","<Side>HandRing1/<Side>HandRing2"},
        {"Hand_Ring1/Hand_Ring2/Hand_Ring3","<Side>HandRing1/<Side>HandRing2/<Side>HandRing3"},
        {"Hand_Pinky0","<Side>HandPinky1"},
        {"Hand_Pinky0/Hand_Pinky1","<Side>HandPinky1/<Side>HandPinky2"},
        {"Hand_Pinky0/Hand_Pinky1/Hand_Pinky2","<Side>HandPinky1/<Side>HandPinky2/<Side>HandPinky3"}

    };

    Dictionary<GameObject,GameObject> goJointMap;

    public bool initialized = false;


    // Start is called before the first frame update
    void Start()
    {
        initialized = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!initialized && OVRHandPrefab.transform.Find("Bones") != null){
            OVRHandRoot = OVRHandPrefab.transform.Find("Bones").Find("Hand_WristRoot").gameObject;
            goJointMap = new Dictionary<GameObject, GameObject>();
            foreach(KeyValuePair<string,string> i in jointMap){
                Debug.Log(i.Key);
                Debug.Log(i.Value.Replace("<Side>",CHandSide.ToString()));
                goJointMap.Add(
                    OVRHandRoot.transform.Find(i.Key).gameObject,
                    AvatarHandRoot.transform.Find(i.Value.Replace("<Side>",CHandSide.ToString())).gameObject
                    );
            }
            initialized = true;
        }
        else if(initialized)
        {
            AvatarHandRoot.transform.rotation = OVRHandRoot.transform.rotation * Quaternion.Euler(rotation1);
            foreach(KeyValuePair<GameObject,GameObject> joints in goJointMap){
                joints.Value.transform.rotation = joints.Key.transform.rotation * Quaternion.Euler(rotation1);
            }
        }

    }
}
