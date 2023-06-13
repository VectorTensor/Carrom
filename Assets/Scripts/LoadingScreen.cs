using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject LevelPlayAds;

    private void Start()
    {

        LevelPlayAds.SetActive(false);
        gameObject.SetActive(true);

        FirebaseSetup.OnSceneChange += ChangeScene;

        Debug.Log("Loading Start");
    }

    void ChangeScene()
    {
        Debug.Log("Change Scene");
        Debug.Log(gameObject.activeSelf);
        gameObject.SetActive(false);
        Debug.Log(gameObject.activeSelf);
        LevelPlayAds.SetActive(true);
    }
}
