using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class Waiter : MonoBehaviour
{
    public static int required_number_of_players=2;
    public int number_of_players;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        number_of_players = PhotonNetwork.CurrentRoom.PlayerCount;
        if (number_of_players >= required_number_of_players){

            SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);            

        }
        
    }
}
