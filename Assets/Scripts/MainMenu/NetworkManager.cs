using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject PlayerName; 

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
        playButton.GetComponent<Button>().interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        playButton.GetComponent<Button>().interactable = FirebaseSetup.fd.isPlayable;
    }

    public void playButtonClicked()
    {
        PhotonNetwork.NickName = PlayerName.GetComponent<TMP_InputField>().text;
        //Debug.Log(PhotonNetwork.NickName);
        //Debug.Log("logged");
        onClickCreateRoom();
    }

    public void onClickCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; //  Change this variable for the max room number 
        PhotonNetwork.JoinRandomOrCreateRoom( null,0, MatchmakingMode.FillRoom,null,null, null,roomOptions );
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
