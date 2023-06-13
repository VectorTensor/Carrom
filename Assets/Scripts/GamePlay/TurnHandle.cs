using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.SceneManagement;

public class TurnHandle : MonoBehaviour , IOnEventCallback
{
    [SerializeField] GameObject UIslider;

    [SerializeField] GameObject forceSlider;

    [SerializeField] GameObject PlayerNames;
    [SerializeField] GameObject PositionSlider;
    public GameObject player;

    List<Vector3> PositionList = new List<Vector3>(){
        new Vector3(360f,200f,0),
        new Vector3(360f,1250f,0),
        new Vector3(-2.01f,0.1f,-0.17f),
        new Vector3(1.56f,0.1f,-0.17f)
    };
    public List<GameObject> PlayerList = new List<GameObject>();

    const byte OBJECTINITIALIZED  = 1;
    
    private int turn=1; 
    private int total_numbers_of_players =2 ;
    // Start is called before the first frame update
    void Start()
    {
        
        total_numbers_of_players = 2;
        turn = 1;
        //PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("helloEveryone",RpcTarget.All,"hello mate");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber) ;
        StartCoroutine(WaitTillPlayersAdded());
       // ActivatePlayerUIComponents();
        
    }

    IEnumerator WaitTillPlayersAdded()
    {
        while (PlayerList.Count < Waiter.required_number_of_players){
            yield return null;
        }
        Debug.Log("Players sucessfully added");

        // code to activate required players and deactivate others

        int index  = 1;        
        foreach (GameObject gm in PlayerList ){

            if (index != this.turn){
                gm.transform.position = new Vector3(0,0,14);
            }
            index++;


            
        }
        ActivatePlayerUIComponents();
        //ActivateRequiredPlayers();

    }

    void ActivateRequiredPlayers(){

    }

    void activateSlider(){
        forceSlider.SetActive(true);
    }

    void savePlayerReference(GameObject gm){

        player = gm; 

        object[] content = new object[] {gm.GetPhotonView().ViewID, gm.name};

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions{ Receivers = ReceiverGroup.All};

        PhotonNetwork.RaiseEvent(OBJECTINITIALIZED, content, raiseEventOptions, SendOptions.SendReliable );



    }

    [PunRPC]
    void messageEveryone(string message){
        Debug.Log("messaged called");
        Debug.Log(message);

    }
    void OnEnable(){
        PhotonNetwork.AddCallbackTarget(this);
        Striker_R.endAction += actionDone;
        ForceDirection.directionGiven += activateSlider; 
        PlayerSpwan.StrikerInstantiated  += savePlayerReference; 
        Exit.PlayerLeftRoom += PlayerLeft;
    }
    void OnDisable(){
        PhotonNetwork.RemoveCallbackTarget(this);
        Striker_R.endAction -= actionDone;
        ForceDirection.directionGiven -= activateSlider; 
        PlayerSpwan.StrikerInstantiated  -= savePlayerReference; 
        Exit.PlayerLeftRoom -= PlayerLeft;
    }

    [PunRPC]
    void sendTurn(int turn){
        int prevTurn = this.turn;
        Debug.Log("turn" + turn);
        this.turn = turn;

        StartCoroutine(WaitTillObjectVelZero(prevTurn)) ;   

        // Before activating and deactivating the players check if physics in completed (check playerobject velocity is 0 )
        //ActivatePlayerUIComponents();

    }

    IEnumerator WaitTillObjectVelZero(int turn)
    {
        //GameObject gm = PlayerList[turn-1];

        yield return new WaitForSeconds(1);
        Debug.Log("Players sucessfully added");


        int index  = 1;        
        foreach (GameObject gm in PlayerList ){

            if (index != this.turn){
                gm.transform.position = new Vector3(0,0,14);
            }
            index ++;


            
        }
        PlayerList[this.turn-1].GetComponent<PlayerObject_p>().OnEnable();
        

        //ActivateRequiredPlayers();
        ActivatePlayerUIComponents();

    }


    [PunRPC]
    void nextTurn(){
        // How next turn is calculated
        turn = turn  % total_numbers_of_players+1; 


        PhotonView photonView = PhotonView.Get(this);
//        photonView.RPC("messageEveryone",RpcTarget.All,"Player turn "+ turn );
            photonView.RPC("sendTurn",RpcTarget.All, turn );

    }
    [PunRPC]
    void RemovePlayer(){

        Debug.Log("Rpc called");
        SceneManager.LoadScene("Waiting");
    }

    void PlayerLeft(){

            Debug.Log("Player left function called");
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("RemovePlayer",RpcTarget.Others);


    }

    void actionDone(){

            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("nextTurn",RpcTarget.MasterClient);

    }
    void ActivatePlayerUIComponents(){
        
        for (int i = 0 ; i <total_numbers_of_players;i++){

            
            PlayerNames.transform.GetChild(i).gameObject.SetActive(turn -1  == i);

        }

        PlayerNames.transform.GetChild(turn -1).gameObject.GetComponent<TextMeshProUGUI>().text = PhotonNetwork.LocalPlayer.Get(turn).NickName;

        PositionSlider.GetComponent<RectTransform>().position = PositionList[PhotonNetwork.LocalPlayer.ActorNumber -1]; 
        UIslider.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);
        PositionSlider.SetActive(turn == PhotonNetwork.LocalPlayer.ActorNumber);

    }

    // Update is called once per frame
    void Update()
    {
        //&& turn == int.Parse(PhotonNetwork.LocalPlayer.ActorNumber)

        
    }
    public void OnEvent(EventData photonEvent){

        byte eventCode = photonEvent.Code;
        
        if (eventCode == OBJECTINITIALIZED ){
            object[] data = (object[]) photonEvent.CustomData;

            int id = (int) data[0];
            string name = (string) data[1];

            Debug.Log("this is the id " +  id);
            GameObject gm = PhotonView.Find(id).gameObject;

            gm.name = name;
            PlayerList.Add(gm);



        }

    }
}
