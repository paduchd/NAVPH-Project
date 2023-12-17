using System;
using UnityEditor;
using UnityEngine;

public class SearchContainer : MonoBehaviour
{
    public static event Action OnContainerSearch;
    public static event Action OnSearchRange;
    public static event Action OnLeaveSearchRange;
    
    private bool searched = false;
    private bool inRange = false;
    
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

    private void Update()
    {
        if (!searched && inRange && Input.GetKeyDown(KeyCode.E))
        {
            OnContainerSearch?.Invoke();
            searched = true;
        }
    }
}
