using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerObject_p : MonoBehaviour
{
    GameObject parent;

    List<Vector3> PositionList = new List<Vector3>(){
        new Vector3(0f,0.1f,-1.1f),
        new Vector3(0,0.1f,2.1f),
        new Vector3(-2.01f,0.1f,-0.17f),
        new Vector3(1.56f,0.1f,-0.17f)
    };

// Create tuple list 
// just like local position add to a property 
// The property is the variable min and max that is in the force direction script of reference gameObject

    List<Vector3> RotationList = new List<Vector3>(){
        new Vector3(0,0,0),
        new Vector3(0,180,0),
        new Vector3(0,270,0),
        new Vector3(0,90,0)
    };

    public void startingPostion()
    {
        gameObject.transform.localPosition = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1];
    }
    
    public void OnEnable()
    {
        UIManager.resetStriker += startingPostion;
        gameObject.transform.localPosition = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1];

        gameObject.transform.localRotation = Quaternion.Euler(RotationList[PhotonNetwork.LocalPlayer.ActorNumber -1]);

    //    Debug.Log("object position intialized");
    }

    void OnDisable()
    {
        UIManager.resetStriker -= startingPostion;
    }

    public void Initialize(){

        parent = GameObject.FindWithTag("GameManager");

        gameObject.transform.parent = parent.transform;

        gameObject.transform.localPosition = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1];

        gameObject.transform.localRotation = Quaternion.Euler(RotationList[PhotonNetwork.LocalPlayer.ActorNumber -1]);

      //  Debug.Log("object position intialized");
    }

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.FindWithTag("GameManager");

        gameObject.transform.parent = parent.transform;

        gameObject.transform.localPosition = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1];

        gameObject.transform.localRotation = Quaternion.Euler(RotationList[PhotonNetwork.LocalPlayer.ActorNumber -1]);

       // Debug.Log("Starto"); 
    }
}
