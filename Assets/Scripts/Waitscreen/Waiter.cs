using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Waiter : MonoBehaviour
{
    public static int required_number_of_players=2;
    public int number_of_players;

    [SerializeField] GameObject PlayerList;
    
    void Start()
    { 
        if (IronSource.Agent.isInterstitialReady() && FirebaseSetup.fd.isShow == true)
            IronSource.Agent.showInterstitial();
        else
            Debug.Log("Interstitial Ad Not Ready");

        PlayerList.transform.GetChild(PhotonNetwork.LocalPlayer.ActorNumber-1).GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName + "(Joined)";
    }

    void Update()
    {
        number_of_players = PhotonNetwork.CurrentRoom.PlayerCount;
        
        if (number_of_players >= required_number_of_players)
            SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);            
    }
}
