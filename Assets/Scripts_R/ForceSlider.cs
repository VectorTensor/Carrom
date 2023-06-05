using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForceSlider : MonoBehaviour
{
    [Header ("Force Animation")]
    float minValue = 0f;
    float maxValue = 1f;
    float speed = 2f;
    bool isIncreasing = true;
    IEnumerator forceBarCoroutine;
    IEnumerator disableStrikerCoroutine;


    public Slider forceSlider;
    public static float currentValue;

    public delegate void onForceSet();
    public static onForceSet onforceset;

    void Update()
    {
        //invoke striker attack function and disable Force Slider
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            Debug.Log("Space Pressed");
            currentValue = forceSlider.value;
            onforceset?.Invoke();
            //gameObject.SetActive(false);
            /*if(disableStrikerCoroutine == null)
            {
                Debug.Log("is null");
                disableStrikerCoroutine = UIManager.instance.DisableStriker();
                StartCoroutine(disableStrikerCoroutine);
            }*/
        }
    }

    void OnEnable()
    {
        if (forceBarCoroutine == null)
        {
            forceBarCoroutine = C_AnimateForceBar();
        }
        StartCoroutine(forceBarCoroutine);
    }

    private IEnumerator C_AnimateForceBar()
    {
        while (true)
        {
            if (isIncreasing)
            {
                forceSlider.value += Time.deltaTime * speed;

                if (forceSlider.value >= maxValue)
                    isIncreasing = false;
            }
            else
            {
                forceSlider.value -= Time.deltaTime * speed;

                if (forceSlider.value <= minValue)
                    isIncreasing = true;
            }
            yield return null;
        }
    }
}
