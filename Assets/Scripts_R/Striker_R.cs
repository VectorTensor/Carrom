using UnityEngine;
using Photon.Pun;

public class Striker_R : MonoBehaviour
{
    public float y;

    [Header ("Striker")]
    Rigidbody rb;
    public Transform stikerTransform;
    public Vector3 strikerPosition;

    [Header ("Striker Force")]
    public float strikerForce;

    [Header("Striker Direction")]
    public Vector3 direction;
    public Vector3 mousePos1;
    public Vector3 mousePos2;
    public LineRenderer lineRenderer;

    public delegate void  EndAction();
    public static EndAction endAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stikerTransform = transform;
        strikerPosition = transform.position;
    }
    void OnEnable() => ForceSlider.onforceset += Attack;

    void Attack()//runs 2times after Spacebar
    {
        //rb.AddForce(direction * force * forceMultipler);
        PhotonView photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            Vector3 force = Vector3.forward * strikerForce * ForceSlider.currentValue;
            rb.AddRelativeForce(force);

            //Disable striker for player1
            //gameObject.SetActive(false);

            //Set player1 has strike true
            //UIManager.hasStriked = true;
            //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber + "hasStriked?" + UIManager.hasStriked);
            
            //Change turn
            endAction?.Invoke();
        }
        else
        {
            Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber + "has not striked hasStriked" + UIManager.hasStriked);
        }
    }

    private void Update()
    {
        mousePos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos2 = new Vector3(-mousePos1.x, y, -mousePos1.z);

        shootStriker();

        lineRenderer.SetPosition(0, strikerPosition);
        lineRenderer.SetPosition(1, -mousePos2);

    }

    public void shootStriker()
    {
        rb.AddForce(direction * strikerForce);
    }
}
