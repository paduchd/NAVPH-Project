using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sewerSceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.tag == "Player")
            {
                Debug.Log("Successfully escaped from garage");
                SceneManager.LoadScene("Sewers");
            }
        }}
