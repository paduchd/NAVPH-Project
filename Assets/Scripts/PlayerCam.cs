using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform lookAt;

    public GameObject thirdPersonCam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 dirToLookAt = lookAt.position - new Vector3(transform.position.x, lookAt.position.y, transform.position.z);
        orientation.forward = dirToLookAt.normalized;

        playerObj.forward = dirToLookAt.normalized * -1;
    }
}
