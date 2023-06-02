using UnityEngine;
using Photon.Pun;
public class Striker_R : MonoBehaviour
{
    public float strikerForce;
    public float magnitude;
    Rigidbody rb;
    Vector3 startPos;
    public delegate void  EndAction();

    public static EndAction endAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void Attack()
    {
        //rb.AddForce(direction * force * forceMultipler);
        PhotonView photonView = GetComponent<PhotonView>();
        Debug.Log("attack");
        if (photonView.IsMine){

        Debug.Log("is mine");
        Vector3 force = Vector3.forward * strikerForce * ForceSlider.currentValue;
        rb.AddRelativeForce(force);
        UIManager.hasStriked = true;
        magnitude = rb.velocity.magnitude;
        endAction?.Invoke();
        Debug.Log("Attacked");
        }
    }

    void OnEnable()
    {
        ForceSlider.onforceset += Attack;
    }
}
