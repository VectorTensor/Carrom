using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
public class GameplayBegin : MonoBehaviour
{
    public int required_number_of_players=2;
    public int number_of_players;

    [SerializeField] GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        game.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        number_of_players = PhotonNetwork.CurrentRoom.PlayerCount;
        if (number_of_players >= required_number_of_players){

            game.SetActive(true);

        }
        
    }
}
