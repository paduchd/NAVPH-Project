using System;
using UnityEngine;

public class SearchContainer : MonoBehaviour
{
    public static event Action OnContainerSearch;
    public static event Action OnSearchRange;
    public static event Action OnLeaveSearchRange;
    
    private bool searched = false;
    private bool inRange = false;
    
    // Event triggers for when player is in range to search container
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !searched)
        {
            inRange = true;
            OnSearchRange?.Invoke();
        }
    }
    private void OnTriggerExit(Collider col){
        if (col.gameObject.CompareTag("Player") && !searched)
        {
            inRange = false;
            OnLeaveSearchRange?.Invoke();
        }
    }

    // Event trigger for when the player searches container
    private void Update()
    {
        if (!searched && inRange && Input.GetKeyDown(KeyCode.E))
        {
            OnContainerSearch?.Invoke();
            searched = true;
        }
    }
}
