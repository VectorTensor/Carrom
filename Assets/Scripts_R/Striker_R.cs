using System.Collections;
using UnityEngine;

public class Striker_R : MonoBehaviour
{
    Rigidbody rb;
    float forceMultipler = 2000f;
    bool confirmButtonPressed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!confirmButtonPressed) //if button not clicked
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    float hitX = Mathf.Clamp(hit.point.x, -1.3f, 1.3f);
                    Vector3 newPostion = new Vector3(hitX, transform.position.y, transform.position.z);
                    rb.MovePosition(newPostion);
                }
            }

        }
        

        if(Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("ForceSlider.currentValue = " + ForceSlider.currentValue);
            Vector3 force = Vector3.forward * ForceSlider.currentValue * forceMultipler;
            rb.AddForce(force);

            //Check if striker is moving or not
            //if yes wait for striker to stop moving 
            //if not StartCoroutine(ResetStrikerPos());
        }
    }

    public void ConfirmButton()
    {
        confirmButtonPressed = true;
    }

    /* IEnumerator ResetStrikerPos()
     {
         yield return new WaitForSeconds(2.0f);
         transform.position = new Vector3(0, 0.1f, -1.7f);
     }*/
}
