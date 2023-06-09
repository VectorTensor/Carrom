using UnityEngine;
using TMPro;
using System.Collections;

public class gameName : MonoBehaviour
{
    TextMeshProUGUI tx;

    void Start()
    {
        tx = GetComponent<TextMeshProUGUI>();
        StartCoroutine("C_SetGameName");
    }

    IEnumerator C_SetGameName()
    {
        yield return new WaitForSeconds(2);
        tx.text = FirebaseSetup.fd.GameName;
        Debug.Log(FirebaseSetup.fd.GameName);
    }
}
