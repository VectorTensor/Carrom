using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    Button button;
    
    public GameObject CreateorJoin;
    public GameObject room;

    //Ads button
    public GameObject displayInterstitial;
    public GameObject displayRewarded;

    //public delegate void OnPlayClicked();
    //public static OnPlayClicked onPlayClicked;

    void Start()
    {
        //gameObject.SetActive(FirebaseSetup.fd.isPlayable);


        button = GetComponent<Button>();
        button.onClick.AddListener(buttonHandler);
    }

    void buttonHandler()
    {
        //CreateorJoin.SetActive(true);
        
        displayInterstitial.SetActive(false);
        displayRewarded.SetActive(false);

        NetworkManager.GetInstance().playButtonClicked();
        //onPlayClicked?.Invoke();
    }
}
