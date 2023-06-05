using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Striker_R : MonoBehaviour
{
    Rigidbody rb;
    public Transform strikerTransform;
    public Vector3 strikerPosition;

    [Header("For Position")]
    public Slider strikerSlider;

    [Header("For Direction")]
    public float y = 0.5f;
    public Vector3 direction;
    public Vector3 mosPosition1, mosPosition2; //mouse position
    public LineRenderer lineRenderer;

    public bool hasStriked = false;
    public bool hasPositionSet = false;

    public float strikerForce;
    //
    public float magnitude;
    public delegate void  EndAction();
    public static EndAction endAction;
    //
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        strikerTransform = transform;
        strikerPosition = transform.position;
        lineRenderer = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        strikerSlider = GameObject.Find("PositionSlider").gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if(!hasStriked && !hasPositionSet)
        {
            strikerTransform.position = new Vector3(strikerSlider.value, 0.1f, strikerPosition.z);
        }

        mosPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mosPosition2 = new Vector3(-mosPosition1.x, y, -mosPosition1.z);

        if (Input.GetMouseButtonUp(0))
        {
            shootStriker();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!hasPositionSet)
            {
                hasPositionSet = true;
            }
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mosPosition2);
    }

    public void shootStriker()
    {
        Debug.Log("Shoot");
        direction = (Vector3)(mosPosition2 - transform.position);
        rb.AddForce(direction * strikerForce);
        hasStriked = true;
    }

    /*void Attack()
    {
        //rb.AddForce(direction * force * forceMultipler);
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView.IsMine){

        Vector3 force = Vector3.forward * strikerForce * ForceSlider.currentValue;
        rb.AddRelativeForce(force);
        UIManager.hasStriked = true;
        magnitude = rb.velocity.magnitude;
        endAction?.Invoke();
        }
    }

    void OnEnable()
    {
        ForceSlider.onforceset += Attack;
    }*/
}
