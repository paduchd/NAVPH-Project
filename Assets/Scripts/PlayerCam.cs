using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rb;
    public float rotationSpeed;
    public Transform lookAt;
    public GameObject thirdPersonCam;
	public float horizontal;
	public float vertical;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 orientationDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = orientationDirection.normalized;
        
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		Vector3 direction = orientation.forward * vertical *-1 + orientation.right * horizontal * -1;

		if(direction.magnitude > 0f)
		{
			playerObject.forward = Vector3.Slerp(playerObject.forward, direction.normalized, Time.deltaTime * rotationSpeed);
		}
    }
}
