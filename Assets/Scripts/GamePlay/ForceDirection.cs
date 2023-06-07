using UnityEngine;
using System;
public class ForceDirection : MonoBehaviour
{
    public float childY;
    public float sensitivity = 9.0f;
    public float minimum = -45.0f;
    public float maximum = 45.0f;
    private float Rot = 0;

    public static event Action directionGiven;

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        Rot += Input.GetAxis("Mouse X") * sensitivity;
     //   Rot = Mathf.Clamp(Rot, minimum, maximum);
        transform.eulerAngles = new Vector3(0, Rot, 0);

        childY = transform.localRotation.eulerAngles.y;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 x = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).position - gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).position; 
            Strikeforce.direction =  x.normalized;
            directionGiven?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
