using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;


public class Waiter : MonoBehaviour
{
    public static int required_number_of_players=2;
    public int number_of_players;

    List<Vector3> PositionList = new List<Vector3>(){
        new Vector3(0f,-95f,0f),
        new Vector3(0,-190f,0),
        new Vector3(-2.01f,0.1f,-0.17f),
        new Vector3(1.56f,0.1f,-0.17f)
    };

    [SerializeField] GameObject PlayerList;
    
    // Start is called before the first frame update
    void Start()
    {



        PlayerList.transform.GetChild(PhotonNetwork.LocalPlayer.ActorNumber-1).GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName + "(Joined)";


    
        



        
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
