using UnityEngine;

public class Striker_R : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                float hitX = Mathf.Clamp(hit.point.x, -3f, 3f);
                Vector3 newPostion = new Vector3(hitX, transform.position.y, transform.position.z);
                rb.MovePosition(newPostion);
            }
        }
    }
}
