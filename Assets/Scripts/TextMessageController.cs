using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMessageController : MonoBehaviour
{
    public string textString;
    public TextMeshProUGUI textElement;
    
    void Start()
    {
        textElement.text = textString;
    }

    
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "EndShelf")
        {
            textElement.text = "You have successfully escaped!";
            GetComponent<PlayerMove>().enabled = false;
        }
    }
}
