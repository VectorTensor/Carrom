using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class TurnHandle : MonoBehaviour , IOnEventCallback
{
    [SerializeField] GameObject UIslider;

    [SerializeField] GameObject forceSlider;

    [SerializeField] GameObject PositionSlider;
    public GameObject player;

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
        ActivatePlayerUIComponents();
        
    }

    IEnumerator WaitTillPlayersAdded()
    {
        while (PlayerList.Count < Waiter.required_number_of_players){
            yield return null;
        }
        Debug.Log("Players sucessfully added");

        // code to activate required players and deactivate others

        //ActivateRequiredPlayers();

    }

    void ActivateRequiredPlayers(){

       int  index = 0; 
       foreach(GameObject gm in PlayerList){

            gm.SetActive(index +1 == turn); // if its the players turn then activate the gameObject
            index ++;
       }  
    }

    void activateSlider(){
        forceSlider.SetActive(true);
    }

    void savePlayerReference(GameObject gm){

        player = gm; 
       // player.GetComponent<PlayerObject_p>().Initialize();

        //player.GetComponent<Collider>().enabled = (turn == PhotonNetwork.LocalPlayer.ActorNumber);
        //player.GetComponent<MeshRenderer>().enabled =(turn == PhotonNetwork.LocalPlayer.ActorNumber);
        //Thread.Sleep(1000);
        //if (turn != PhotonNetwork.LocalPlayer.ActorNumber){

            //Debug.Log("gone");
            //player.transform.position= new Vector3(0,0,14);
        //}

        // Create an event that calls other client send the gameobject photonView id. In client the photon View id will be used to get refence 
        // of the playerObject and it will be stored in the PlayerList

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
    }
    void OnDisable(){
        PhotonNetwork.RemoveCallbackTarget(this);
        Striker_R.endAction -= actionDone;
        ForceDirection.directionGiven -= activateSlider; 
        PlayerSpwan.StrikerInstantiated  -= savePlayerReference; 
    }

    [PunRPC]
    void sendTurn(int turn){
        Debug.Log("turn" + turn);
        this.turn = turn;

        // Before activating and deactivating the players check if physics in completed (check playerobject velocity is 0 )
        //ActivateRequiredPlayers();
        ActivatePlayerUIComponents();
    }
    [PunRPC]
    void nextTurn(){
        // How next turn is calculated
        turn = turn  % total_numbers_of_players+1; 


         Debug.Log("nextTurn function called");
        PhotonView photonView = PhotonView.Get(this);
//        photonView.RPC("messageEveryone",RpcTarget.All,"Player turn "+ turn );
            photonView.RPC("sendTurn",RpcTarget.All, turn );



        


    }

    void actionDone(){

            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("nextTurn",RpcTarget.MasterClient);

    }
    void ActivatePlayerUIComponents(){
        

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
