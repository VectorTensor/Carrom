using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x=  Mathf.Clamp(transform.position.x,-2.15f, 1.75f);       
        float z=  Mathf.Clamp(transform.position.z,-2.45f, 1.5f);       

        float y=  Mathf.Clamp(transform.position.y,0, 2f);       
        transform.position = new Vector3(x, y, z);

        
    }
}
