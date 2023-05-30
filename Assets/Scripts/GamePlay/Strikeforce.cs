using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strikeforce : MonoBehaviour
{
    public static float magnitude;
    public static Vector3 direction;
    

    public static Vector3 getForce(){

        return magnitude*direction;

    }

    // Start is called before the first frame update
}
