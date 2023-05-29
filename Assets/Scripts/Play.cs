using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    Button button;
    public GameObject CreateorJoin;

    public GameObject room;
    // Start is called before the first frame update
    void Start()
    {
        
        room.SetActive(false);
        CreateorJoin.SetActive(false);
        button = GetComponent<Button>();
        button.onClick.AddListener(buttonHandler);

        
    }

    private void buttonHandler(){
        

        CreateorJoin.SetActive(true);            

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
