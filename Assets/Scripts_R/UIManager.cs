using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class UIManager : MonoBehaviour
{
    public float y; 
    
    GameObject arrow, selfTrans;
    Rigidbody strikerRb;

    public Slider positionSlider;
    public Slider forceSlider;
    public static bool hasStriked = false;

    private void Start()
    {
        // Find which player are you and find the respective slider
        selfTrans = GameObject.Find("Striker" + PhotonNetwork.LocalPlayer.ActorNumber);
        strikerRb = selfTrans.GetComponent<Rigidbody>();
        arrow = selfTrans.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            forceSlider.gameObject.SetActive(false);
            Debug.Log("Force bar = " + forceSlider.gameObject.activeSelf);
        }

        //Reset striker position
        if (strikerRb.velocity.magnitude == 0.0f && hasStriked)
        {
            ResetStrikerPosition();
        }

        //Set striker postion with slider
        if (positionSlider.gameObject.activeSelf == true )
        {
            selfTrans.transform.position = new Vector3(positionSlider.value, selfTrans.transform.position.y, selfTrans.transform.position.z);
        }

        //Set direction //Correction Needed
        y = arrow.transform.localRotation.eulerAngles.y; //30
        selfTrans.transform.localRotation =  Quaternion.Euler(new Vector3(0, y, 0));  //30
    }

    public void ConfirmButton()
    {
        positionSlider.gameObject.SetActive(false);
        arrow.SetActive(!arrow.activeSelf);
        forceSlider.gameObject.SetActive(true);
    }

    public void ResetStrikerPosition()
    {
        Debug.Log("Reset Striker Position");
        selfTrans.transform.position = new Vector3(0, 0.1f, -1.75f);
        positionSlider.gameObject.SetActive(true);
        hasStriked = false;
    }
}
