using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxTrapController : MonoBehaviour
{
    public string textString;
    //public TextMeshProUGUI textElement;
    
    void Start()
    {
        //textElement.text = textString;
    }

    
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "GarageFloor")
        {
            Debug.Log("Box collided with the ground");
            //textElement.text = "You have alerted your kidnappers!\nQuickly find a way out!";
        }
    }
}
