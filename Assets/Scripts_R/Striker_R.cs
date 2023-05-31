using UnityEngine;

public class Striker_R : MonoBehaviour
{
    public float y;
    public float strikerForce;

    Rigidbody rb;
    bool confirmPosition = false;

    [SerializeField] GameObject confirmButton;
    GameObject arrow ;

    private void Start()
    {
        arrow = gameObject.transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (!confirmPosition) //if button not clicked
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

        //Set direction
        y = arrow.transform.localRotation.eulerAngles.y;
        transform.localEulerAngles = new Vector3(0, y, 0);

        //Add force
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Added Force");
            rb.AddRelativeForce(Vector3.forward * strikerForce * ForceSlider.currentValue);
        }
    }

    /*void OnEnable()
    {
        ForceSlider.onforceset += Attack;
    }

    void Attack()
    {
        Debug.Log("Add force ");
        Rigidbody rb = GetComponent<Rigidbody>();
    }*/

    public void ConfirmButton()
    {
        confirmPosition = true;
        confirmButton.SetActive(false);
        arrow.SetActive(!arrow.activeSelf);
    }

    /* IEnumerator ResetStrikerPos()
     {
         yield return new WaitForSeconds(2.0f);
         transform.position = new Vector3(0, 0.1f, -1.7f);
     }*/
}
