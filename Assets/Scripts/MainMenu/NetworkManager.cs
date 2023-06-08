using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private int numberOfPlayers;
    GameObject playButton;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public static NetworkManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        playButton = GameObject.Find("Play");
        playButton.GetComponent<Button>().interactable = false;
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        playButton.GetComponent<Button>().interactable = true;
    }

    public void playButtonClicked()
    {
        PhotonNetwork.NickName = GameObject.Find("PlayerName").GetComponent<TMP_InputField>().text;
        //Debug.Log(PhotonNetwork.NickName);
        //Debug.Log("logged");
    }

    public void onClickCreateRoom(string name)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = numberOfPlayers;
        PhotonNetwork.JoinOrCreateRoom(name,options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room = ",this);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed "+ message, this);
    }

    public void onClickJoinRoom(string name){

        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }
}
