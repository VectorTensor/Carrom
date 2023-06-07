using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerSpwan : MonoBehaviour 
{
    [SerializeField] GameObject playerPrefab;

    public static event Action<GameObject> StrikerInstantiated;


    const byte OBJECTINITIALIZED  = 1;
    void Start()
    {
        GameObject gm = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        gm.name = "Striker" + PhotonNetwork.LocalPlayer.ActorNumber;

        //Debug.Log("view id " + gm.GetPhotonView().ViewID); 


        StrikerInstantiated?.Invoke(gm);
   
    }


}
