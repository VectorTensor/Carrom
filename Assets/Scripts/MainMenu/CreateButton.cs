using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateButton: MonoBehaviour
{
    // Start is called before the first frame update


    public static void listener(){
        
        GameObject gm = GameObject.Find("roomName");
        TMP_InputField text = gm.GetComponent<TMP_InputField>(); 
        NetworkManager.GetInstance().onClickCreateRoom(text.text);

        Debug.Log(text.text);
    }
}
