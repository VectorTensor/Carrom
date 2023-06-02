using UnityEngine;

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
        Vector3 force = Vector3.forward * strikerForce * ForceSlider.currentValue;
        rb.AddRelativeForce(force);
        UIManager.hasStriked = true;
        magnitude = rb.velocity.magnitude;
        endAction?.Invoke();
    }

    void OnEnable()
    {
        ForceSlider.onforceset += Attack;
    }
}
