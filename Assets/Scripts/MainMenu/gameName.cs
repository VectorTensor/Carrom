using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameName : MonoBehaviour
{
    Text tx;

    void Start()
    {
        tx = GetComponent<Text>();
        StartCoroutine("C_SetGameName");
    }

    IEnumerator C_SetGameName()
    {
        yield return new WaitForSeconds(2.0f);
        tx.text = FirebaseSetup.fd.GameName;
        Debug.Log(FirebaseSetup.fd.GameName);
    }
}
