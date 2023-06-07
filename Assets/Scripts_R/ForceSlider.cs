using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForceSlider : MonoBehaviour
{
    float forceBarSpeed = 300f;

    float minValue = 0f;
    float maxValue = 200f;
     
    int forceValueMultipler = 100;

    bool isIncreasing = true;
    
    IEnumerator coroutine;

    public Slider forceSlider;

    public static float currentValue;

    public delegate void onForceSet();
    public static onForceSet onforceset;

    public float count;


    void OnEnable()
    {
        if(coroutine == null)
        {
            coroutine = AnimateForceBar();
        }
        StartCoroutine(coroutine);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Force added");
            currentValue = forceSlider.value * forceValueMultipler; 
            Strikeforce.magnitude = currentValue;
            onforceset?.Invoke();
            forceSlider.gameObject.SetActive(false);
        }
    }

    private IEnumerator AnimateForceBar()
    {
        while (true)
        {
            if (isIncreasing)
            {
                forceSlider.value += Time.deltaTime * forceBarSpeed;

                if (forceSlider.value >= maxValue)
                    isIncreasing = false;
            }
            else
            {
                forceSlider.value -= Time.deltaTime * forceBarSpeed;

                if (forceSlider.value <= minValue)
                    isIncreasing = true;
            }
            yield return null;
        }
    }
}
