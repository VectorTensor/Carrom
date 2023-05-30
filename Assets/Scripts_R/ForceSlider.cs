using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ForceSlider : MonoBehaviour
{
    public Slider forceSlider;

    float minValue = 0f;
    float maxValue = 1f;
    float speed = 2f;

    bool isIncreasing = true;
    bool isRunning = true;

    IEnumerator coroutine;

    public static float currentValue;

    private void Start()
    {
        coroutine = AnimateForceBar();
        StartCoroutine(coroutine);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = !isRunning;

            if(isRunning)
            {
                StartCoroutine(coroutine);
            }
            else
            {
                StopCoroutine(coroutine);
            }

            currentValue = forceSlider.value;
            Debug.Log("Current Value = " + currentValue);
        }
    }

    private IEnumerator AnimateForceBar()
    {
        while(true)
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
