using UnityEngine;

public class RemoteValueTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Log", 5f, 2f);
    }    

    void Log()
    {
        Debug.Log("Test = " + FirebaseSetup.fd.isPlayable);
    }
}
