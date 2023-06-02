using UnityEngine;
using Photon.Pun;
using System.Collections;

public class TurnHandle : MonoBehaviour
{
    int turn; 
    int total_numbers_of_players = 2 ;

    public GameObject hidder;

    void Start()
    {
        total_numbers_of_players = 2;
        turn = 1;

        //PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("helloEveryone",RpcTarget.All,"hello mate");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber) ;

        hidder.SetActive(true);
    }
   
    void Update()
    {
        //&& turn == int.Parse(PhotonNetwork.LocalPlayer.ActorNumber)
        //hasStrike = true
        if (turn == PhotonNetwork.LocalPlayer.ActorNumber ) 
        {
            //enable UI
            hidder.SetActive(false);

            //PhotonView photonView = PhotonView.Get(this);
            //photonView.RPC("nextTurn",RpcTarget.MasterClient);
        }
        else
        {
            //enable UI
            hidder.SetActive(true);
        }
    }

    [PunRPC]
    void nextTurn()
    {
        // How next turn is calculated
        turn = turn  % total_numbers_of_players+1;

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("messageEveryone", RpcTarget.All, "Player turn "+ turn);

        photonView.RPC("sendTurn", RpcTarget.Others, turn);

        StartCoroutine("SetTrue");
    }

    [PunRPC]
    void messageEveryone(string message)
    {
        Debug.Log(message);
    }

    [PunRPC]
    void sendTurn(int turn)
    {
        this.turn = turn;
    }

    IEnumerable SetTrue()
    {
        yield return new WaitForSeconds(2);
        hidder.SetActive(true);
    }
}
