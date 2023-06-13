using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonHandler);
    }

    void buttonHandler()
    {
        //CreateorJoin.SetActive(true);
        NetworkManager.GetInstance().playButtonClicked();
        //onPlayClicked?.Invoke();
    }
}
