using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class BoxTrapController : MonoBehaviour
{
    public float countdownTime;
    public TextMeshProUGUI countdownUI;
    public TextMeshProUGUI objective;
    private bool isBoxDown = false;
    private bool isEnabled = true;
    private float min;
    private float sec;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isEnabled)
        {
            if (isBoxDown)
            {
                min = Mathf.FloorToInt((countdownTime - Time.deltaTime) / 60);
                sec = Mathf.FloorToInt((countdownTime - Time.deltaTime) % 60);
                countdownUI.text = string.Format("{0:00}:{1:00}", min, sec);
                objective.text = "Use the box to escape before the timer runs out!";
                countdownTime -= Time.deltaTime;
            }

            if (isBoxDown && countdownTime <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    
    public void DisableTimer()
    {
        isEnabled = false;
        
        countdownUI.text = "";
        objective.text = "Get to the sewers entrance";
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "GarageFloor")
        {
            Debug.Log("Box collided with the ground");
            isBoxDown = true;
        }
    }
}
