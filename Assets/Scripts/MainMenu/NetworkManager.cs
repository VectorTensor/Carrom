using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private int numberOfPlayers;
    [SerializeField]
    // Start is called before the first frame update

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    public static NetworkManager GetInstance(){
        return instance;
    }

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

    public void playButtonClicked(){

        PhotonNetwork.NickName = GameObject.Find("PlayerName").GetComponent<TMP_InputField>().text;
        //Debug.Log(PhotonNetwork.NickName);
        //Debug.Log("logged");
        onClickCreateRoom();
    }

    public void onClickCreateRoom(){

        PhotonNetwork.JoinRandomOrCreateRoom();

    }

    public override void OnCreatedRoom()
    {

        Debug.Log("Created room",this);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed "+ message, this);

    }


    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Waiting");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
