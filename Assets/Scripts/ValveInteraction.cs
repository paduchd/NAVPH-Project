using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValveInteraction : MonoBehaviour
{

    [SerializeField] private GameObject Water;
    
    [SerializeField] private TextMeshProUGUI UIInteractionText;

    private bool IsInInteractionRange = false;

    private bool Activated = false;


    // Update is called once per frame
    // OnTriggerStay can be used as well, however it would require collider size and shape change
    void Update()
    {
        // Interaction check
        if(IsInInteractionRange && !Activated && Input.GetKeyDown(KeyCode.E)){
            Activated = true;
            Vector3 WaterPosition = Water.gameObject.transform.position;
            WaterPosition.y += 0.2f;
            Water.gameObject.transform.position = WaterPosition;
        }
    }


    private void OnTriggerEnter(Collider CollisionObject){
        if (CollisionObject.gameObject.CompareTag("Player")){
            IsInInteractionRange = true;
            UIInteractionText.enabled = true;
            UIInteractionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider CollisionObject){
        if (CollisionObject.gameObject.tag == "Player"){
            IsInInteractionRange = false;
            UIInteractionText.gameObject.SetActive(false);
        }
    }
}
