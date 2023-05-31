using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    static int no_of_players = 2;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    public static void playButtonClicked(){

        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("logged");
    }

    public static void onClickCreateRoom(string name){

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = no_of_players;
        PhotonNetwork.JoinOrCreateRoom(name,options, TypedLobby.Default);

    }

    public override void OnCreatedRoom()
    {

        Debug.Log("Created room",this);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed "+ message, this);

    }

    public static void onClickJoinRoom(string name){

        PhotonNetwork.JoinRoom(name);


    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
