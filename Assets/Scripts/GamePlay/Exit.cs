using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;

public class Exit : MonoBehaviour
{
    public static event Action PlayerLeftRoom;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(leaveRoom);
        
    }
    void leaveRoom(){

        PhotonNetwork.LeaveRoom();
        PlayerLeftRoom?.Invoke();

        SceneManager.LoadScene("SampleScene");
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
