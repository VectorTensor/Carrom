using UnityEngine;
using Photon.Pun;

public class TurnHandle : MonoBehaviour
{
    public static TurnHandle instance;

    [SerializeField] GameObject UIslider; //local
    GameObject _striker; //local

    public int turn; 
    int total_numbers_of_players =2 ;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        total_numbers_of_players = 2;
        turn = 1;
        //PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("helloEveryone",RpcTarget.All,"hello mate");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);

        //PlayerSpwan.Striker += OnStrike;
    }

    public void OnStrike()
    {
        _striker = GameObject.Find("Striker" + PhotonNetwork.LocalPlayer.ActorNumber);
    }

    void Update()
    {
        //&& turn == int.Parse(PhotonNetwork.LocalPlayer.ActorNumber)
        UIslider.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);
        //_striker.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);
    }

    
    void OnEnable()
    {
        Striker_R.endAction += actionDone;
    }
    void OnDisable()
    {
        Striker_R.endAction -= actionDone;
    }

    void actionDone()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("nextTurn",RpcTarget.All);
    }

    [PunRPC]
    void nextTurn()
    {
        //UIManager.hasStriked = false;

        // How next turn is calculated
        turn = turn  % total_numbers_of_players+1;

        Debug.Log("nextTurn function called");
        PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("messageEveryone",RpcTarget.All,"Player turn "+ turn );
        photonView.RPC("sendTurn", RpcTarget.Others, turn);
    }

    [PunRPC]
    void sendTurn(int turn)
    {
        Debug.Log("turn" + turn);
        this.turn = turn;
    }

    /*[PunRPC]
    void messageEveryone(string message)
    {
        Debug.Log("messaged called");
        Debug.Log(message);
    }*/

    
}
