using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject playButton;

    public static NetworkManager instance;
    private int numberOfPlayers;
    [SerializeField] GameObject PlayerName; 
    // Start is called before the first frame update

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
        //playButton.GetComponent<Button>().interactable = true;        
        

    }

    public void playButtonClicked(){

        PhotonNetwork.NickName = PlayerName.GetComponent<TMP_InputField>().text;
        //Debug.Log(PhotonNetwork.NickName);
        //Debug.Log("logged");
        onClickCreateRoom();
    }

    public void onClickCreateRoom(){
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
