using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(leaveRoom);
        
    }
    void leaveRoom(){

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("SampleScene");
        
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
