using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurnHandle : MonoBehaviour
{
    private int turn; 
    private int total_numbers_of_players =2 ;
    // Start is called before the first frame update
    void Start()
    {
        
        total_numbers_of_players = 2;
        turn = 1;
        //PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("helloEveryone",RpcTarget.All,"hello mate");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber) ;
        
    }
    [PunRPC]
    void messageEveryone(string message){
        Debug.Log(message);

    }

    [PunRPC]
    void sendTurn(int turn){
        this.turn = turn;
    }
    [PunRPC]
    void nextTurn(){
        // How next turn is calculated
        turn = turn  % total_numbers_of_players+1; 

        
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("messageEveryone",RpcTarget.All,"Player turn "+ turn );
        
        photonView.RPC("sendTurn",RpcTarget.Others, turn );


    }

    // Update is called once per frame
    void Update()
    {
        //&& turn == int.Parse(PhotonNetwork.LocalPlayer.ActorNumber)

        if (Input.GetMouseButtonDown(0) && turn == PhotonNetwork.LocalPlayer.ActorNumber ) {

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("nextTurn",RpcTarget.MasterClient);
        }
        
    }
}
