using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitinfo,0.25f, _layerMask))
        {
            Debug.DrawRay(transform.position,Vector3.down * hitinfo.distance,Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position,Vector3.down * 0.25f,Color.green);
        }
        
    }
}
