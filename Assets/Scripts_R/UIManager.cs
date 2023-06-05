using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public float y; 
    
    GameObject striker, arrow;

    public Slider positionSlider;
    public Slider forceSlider;
    public static bool hasStriked = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void OnEnable(){

        PlayerSpwan.StrikerInstantiated += OnStrikerInstantiated;
    }
    void OnStrikerInstantiated()
    {
        // Find which player it is and find the respective slider
        // Access the "striker" GameObject
        striker = GameObject.Find("Striker" + PhotonNetwork.LocalPlayer.ActorNumber);
        arrow = striker.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (positionSlider.gameObject.activeSelf == true )
        {
            striker.transform.position = new Vector3(positionSlider.value, striker.transform.position.y, striker.transform.position.z);
        }

        //Set direction //Correction Needed
        y = arrow.transform.localRotation.eulerAngles.y; //30
        striker.transform.localRotation =  Quaternion.Euler(new Vector3(0, y, 0));  //30
    }

    public void ConfirmButton()
    {
        //positionSlider.gameObject.SetActive(false);
        arrow.SetActive(!arrow.activeSelf);
        forceSlider.gameObject.SetActive(true);
    }

    public IEnumerator DisableStriker()
    {
        /*Debug.Log("Going to disable now" + TurnHandle.instance.turn); //2
        Debug.Log("Going to disable now" + PhotonNetwork.LocalPlayer.ActorNumber); //1
        bool value = TurnHandle.instance.turn == PhotonNetwork.LocalPlayer.ActorNumber;
        
        striker.SetActive(TurnHandle.instance.turn == PhotonNetwork.LocalPlayer.ActorNumber);*/
        Debug.Log("Going to disable");
        yield return null;
    }
}
