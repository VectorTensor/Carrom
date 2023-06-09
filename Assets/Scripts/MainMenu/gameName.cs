using UnityEngine;
using TMPro;

public class gameName : MonoBehaviour
{
    void Start()
    {
       TextMeshProUGUI tx = GetComponent<TextMeshProUGUI>();
       tx.text = FirebaseSetup.fd.GameName; 
        Debug.Log(FirebaseSetup.fd.GameName);
    }
}
