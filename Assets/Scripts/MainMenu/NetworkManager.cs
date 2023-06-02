using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    static int no_of_players = 2;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject button = GameObject.Find("Play");
        button.GetComponent<Button>().interactable = false;
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        GameObject button = GameObject.Find("Play");
        button.GetComponent<Button>().interactable = true;
        
        

    }

    public static void playButtonClicked(){

        PhotonNetwork.NickName = GameObject.Find("PlayerName").GetComponent<TMP_InputField>().text;
        //Debug.Log(PhotonNetwork.NickName);
        //Debug.Log("logged");
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
