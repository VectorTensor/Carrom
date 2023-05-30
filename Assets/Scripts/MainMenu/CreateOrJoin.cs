using System.Globalization;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateOrJoin : MonoBehaviour
{

    public Button Join;
    public Button Create;
    public GameObject room;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    void OnEnable(){


        Create.onClick.AddListener(CreatebuttonHandler);

        Join.onClick.AddListener(JoinbuttonHandler);

        room.SetActive(false);
    }

    private void JoinbuttonHandler(){
        // Whem Join button is pressed  
        gameObject.SetActive(false);

        room.SetActive(true);
        TMPro.TextMeshProUGUI text= GameObject.Find("JorC").GetComponent<TMPro.TextMeshProUGUI>();;
        text.text = "Join";

        TMPro.TextMeshProUGUI button_text= GameObject.Find("JCButton").GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        button_text.text = "Join";
        Button jcButton = GameObject.Find("JCButton").GetComponent<Button>();        
        jcButton.onClick.AddListener(JoinButton.listener);

    

    }

    private void CreatebuttonHandler(){
        // When create button is pressed

        room.SetActive(true);
        gameObject.SetActive(false);

        TMPro.TextMeshProUGUI text= GameObject.Find("JorC").GetComponent<TMPro.TextMeshProUGUI>();;
        text.text = "Create";
        TMPro.TextMeshProUGUI button_text= GameObject.Find("JCButton").GetComponentInChildren<TMPro.TextMeshProUGUI>(true);;
        button_text.text = "Create";
        Button jcButton = GameObject.Find("JCButton").GetComponent<Button>();        
        jcButton.onClick.AddListener(CreateButton.listener);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
