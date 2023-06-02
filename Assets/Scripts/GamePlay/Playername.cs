using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Playername : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string name = PhotonNetwork.CurrentRoom.GetPlayer(1).ToString (); 
        Debug.Log(name);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
