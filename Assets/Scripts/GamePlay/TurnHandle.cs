using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurnHandle : MonoBehaviour
{
    [SerializeField] GameObject UIslider;
    private int turn; 
    private int total_numbers_of_players =2 ;

    public GameObject gm ;
    // Start is called before the first frame update
    void Start()
    {
        total_numbers_of_players = gm.GetComponent<GameplayBegin>().number_of_players;
        turn = 1;
        //PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("helloEveryone",RpcTarget.All,"hello mate");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber) ;
       // UIslider.SetActive(false);
        
    }
    void onPlayerInstantiation(){

        UIslider.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);
    }
    [PunRPC]
    void messageEveryone(string message){
        Debug.Log("messaged called");
        Debug.Log(message);

    }
    void OnEnable(){
        Striker_R.endAction += actionDone;
        PlayerSpwan.StrikerInstantiated += onPlayerInstantiation;
    }
    void OnDisable(){
        Striker_R.endAction -= actionDone;
        PlayerSpwan.StrikerInstantiated -= onPlayerInstantiation;
    }

    [PunRPC]
    void sendTurn(int turn){
       // Debug.Log("turn" + turn);
        this.turn = turn;
        UIslider.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);
    }
    [PunRPC]
    void nextTurn(){
        // How next turn is calculated
        turn = turn  % total_numbers_of_players+1; 


         Debug.Log("nextTurn function called");
        PhotonView photonView = PhotonView.Get(this);
//        photonView.RPC("messageEveryone",RpcTarget.All,"Player turn "+ turn );
        photonView.RPC("sendTurn",RpcTarget.Others, turn );



        


    }

    void actionDone(){

            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("nextTurn",RpcTarget.MasterClient);
    }

    

    // Update is called once per frame
    void Update()
    {
        //&& turn == int.Parse(PhotonNetwork.LocalPlayer.ActorNumber)

        //`    UIslider.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);
        
    }
}
