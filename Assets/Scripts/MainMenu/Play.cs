using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    Button button;
    
    public GameObject CreateorJoin;
    public GameObject room;

    //public delegate void OnPlayClicked();
    //public static OnPlayClicked onPlayClicked;
    
    void Start()
    {
        
        //gameObject.SetActive(FirebaseSetup.fd.isPlayable);

        room.SetActive(false);
        CreateorJoin.SetActive(false);

        button = GetComponent<Button>();
        button.onClick.AddListener(buttonHandler);
    }

    void buttonHandler()
    {
        CreateorJoin.SetActive(true);
        NetworkManager.GetInstance().playButtonClicked();
        //onPlayClicked?.Invoke();
    }
}
