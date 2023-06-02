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



    
    // Start is called before the first frame update
    void Start()
    {

        parent = GameObject.Find("GameManager");

        gameObject.transform.parent = parent.transform;

        gameObject.transform.localPosition = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1];


        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
