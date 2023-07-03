using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;

public class Exit : MonoBehaviour
{
    public static event Action PlayerLeftRoom;
    
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(leaveRoom);
    }

    void leaveRoom()
    {
        if (IronSource.Agent.isRewardedVideoAvailable() && FirebaseSetup.fd.isShow == true)
            IronSource.Agent.showRewardedVideo();
        else
            Debug.Log("Rewarded Ad Not Ready OR Disabled");

        PhotonNetwork.LeaveRoom();
        PlayerLeftRoom?.Invoke();

        SceneManager.LoadScene("MainMenu");
    }
}
