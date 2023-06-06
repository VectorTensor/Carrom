using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerSpwan : MonoBehaviour, IOnEventCallback
{
    [SerializeField] GameObject playerPrefab;

    public static event Action<GameObject> StrikerInstantiated;


    const byte OBJECTINITIALIZED  = 1;
    void Start()
    {
        GameObject gm = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        gm.name = "Striker" + PhotonNetwork.LocalPlayer.ActorNumber;

        //Debug.Log("view id " + gm.GetPhotonView().ViewID); 

        object[] content = new object[] {gm.GetPhotonView().ViewID};

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions{ Receivers = ReceiverGroup.All};

        PhotonNetwork.RaiseEvent(OBJECTINITIALIZED, content, raiseEventOptions, SendOptions.SendReliable );

        StrikerInstantiated?.Invoke(gm);
   
    }


    void OnEnable(){
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable(){
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent){

        byte eventCode = photonEvent.Code;
        
        if (eventCode == OBJECTINITIALIZED ){
            object[] data = (object[]) photonEvent.CustomData;

            int id = (int) data[0];

            Debug.Log("this is the id " +  id);

        }

    }
   

}
