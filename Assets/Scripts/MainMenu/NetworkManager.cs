using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button playButton;

    public static NetworkManager instance;

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
        playButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        playButton.interactable = FirebaseSetup.fd.isPlayable;
    }

    public void playButtonClicked()
    {
        PhotonNetwork.NickName = GameObject.Find("PlayerName").GetComponent<TMP_InputField>().text;
        //Debug.Log(PhotonNetwork.NickName);
        //Debug.Log("logged");
        onClickCreateRoom();
    }

    public void onClickCreateRoom()
    {
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
}
