using UnityEngine;

public class ForceDirection : MonoBehaviour
{
    public float childY;
    public float sensitivity = 9.0f;
    public float minimum = -45.0f;
    public float maximum = 45.0f;
    private float Rot = 0;

    [SerializeField] GameObject force_slider;

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
        transform.localEulerAngles = new Vector3(0, Rot, 0);

        childY = transform.localRotation.eulerAngles.y;

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            force_slider.SetActive(true);
        }
    }
}
