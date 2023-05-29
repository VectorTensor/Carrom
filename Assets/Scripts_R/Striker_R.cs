using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker_R : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z);
            }
        }
        
    }
}
