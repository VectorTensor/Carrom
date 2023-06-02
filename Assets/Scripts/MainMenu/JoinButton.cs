using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoinButton : MonoBehaviour
{
    public static void listener(){
        GameObject gm = GameObject.Find("roomName");
        TMP_InputField text = gm.GetComponent<TMP_InputField>(); 

        NetworkManager.GetInstance().onClickJoinRoom(text.text);

        Debug.Log(text.text);
        
    }
}
