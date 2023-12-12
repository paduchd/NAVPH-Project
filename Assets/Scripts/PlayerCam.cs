using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
	
	[Header("Camera death mode")]
	[SerializeField] private CinemachineFreeLook freeCameraLook;
	[SerializeField] private float amplitude = 1;
	[SerializeField] private float frequency = 1;
	private PostProcessLayer postProcessLayer;
	private PostProcessVolume postProcessVolume;
	
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        postProcessLayer = GetComponent<PostProcessLayer>();
        postProcessVolume = GetComponent<PostProcessVolume>();
    }

    private void OnEnable()
    {
	    PlayerEventManager.OnDeath += ShowDeathCameraMode;
    }

    private void OnDisable()
    {
	    PlayerEventManager.OnDeath -= ShowDeathCameraMode;
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
    
    private void ShowDeathCameraMode()
    {
	    //show scene in greyscale
	    postProcessLayer.enabled = true;
	    postProcessVolume.enabled = true;
	    
	    CinemachineBasicMultiChannelPerlin channelPerlin0 =
		    freeCameraLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	    CinemachineBasicMultiChannelPerlin channelPerlin1 =
		    freeCameraLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	    CinemachineBasicMultiChannelPerlin channelPerlin2 =
		    freeCameraLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	    
	    channelPerlin0.m_AmplitudeGain = amplitude;
	    channelPerlin0.m_FrequencyGain = frequency;
	    channelPerlin1.m_AmplitudeGain = amplitude;
	    channelPerlin1.m_FrequencyGain = frequency;
	    channelPerlin2.m_AmplitudeGain = amplitude;
	    channelPerlin2.m_FrequencyGain = frequency;
    }
}
