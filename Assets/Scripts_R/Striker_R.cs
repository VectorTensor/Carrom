using System.Collections;
using UnityEngine;

public class Striker_R : MonoBehaviour
{
    Rigidbody rb;
    bool confirmButtonPressed = false;
   [SerializeField] GameObject confirmButton;
    [SerializeField]GameObject arrow ;
    private void Start()
    {
        arrow.SetActive(false);
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
        

    }

    void OnEnable(){
        
        ForceSlider.onforceset += Attack;

    }

    void Attack(){

     Debug.Log("Add force ");
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.AddForce(Strikeforce.getForce());

    }

    public void ConfirmButton()
    {
        confirmButtonPressed = true;
        confirmButton.SetActive(false); 
        arrow.SetActive(true);
    }

    /* IEnumerator ResetStrikerPos()
     {
         yield return new WaitForSeconds(2.0f);
         transform.position = new Vector3(0, 0.1f, -1.7f);
     }*/
}
