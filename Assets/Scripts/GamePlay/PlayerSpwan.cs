using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpwan : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject gm = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        gm.name = "Striker" + PhotonNetwork.LocalPlayer.ActorNumber; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
