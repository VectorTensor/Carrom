using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerObject_p : MonoBehaviour
{
    GameObject parent;
    List<Vector3> PositionList = new List<Vector3>(){
        new Vector3(0f,0.1f,-1.84f),
        new Vector3(0,0.1f,1.67f),
        new Vector3(-2.01f,0.1f,-0.17f),
        new Vector3(1.56f,0.1f,-0.17f)
    };


// Create tuple list 
// just like local position add to a property 
// The property is the variable min and max that is in the force direction script of reference gameObject

    List<Vector2> InitialRotationList = new List<Vector2>(){
        new Vector2(-30,30),
        new Vector2(-120,120), 
        new Vector2(240,300),
        new Vector2(60,120), 
    };

    List<Vector3> RotationList = new List<Vector3>(){
        new Vector3(0,0,0),
        new Vector3(0,180,0),
        new Vector3(0,270,0),
        new Vector3(0,90,0)
    };
    
    // Start is called before the first frame update
    void Start()
    {

        parent = GameObject.FindWithTag("GameManager");

        gameObject.transform.parent = parent.transform;

        gameObject.transform.localPosition = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1];

        ForceDirection reference = GetComponentInChildren<ForceDirection>();

        reference.minimum = InitialRotationList[PhotonNetwork.LocalPlayer.ActorNumber -1].x;
        
        reference.maximum= InitialRotationList[PhotonNetwork.LocalPlayer.ActorNumber -1].y;

        
        transform.Rotate (RotationList[PhotonNetwork.LocalPlayer.ActorNumber -1]);




        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
