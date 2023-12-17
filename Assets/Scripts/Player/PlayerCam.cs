using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Racoon forward direction based on mouse direction + death camera grayscale overlay
public class PlayerCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObject;
    [SerializeField] private float rotationSpeed;
	private float horizontal;
	private float vertical;
	
	[Header("Camera death mode")]
	[SerializeField] private CinemachineFreeLook freeCameraLook;
	[SerializeField] private float amplitude = 1;
	[SerializeField] private float frequency = 1;
	private PostProcessLayer postProcessLayer;
	private PostProcessVolume postProcessVolume;
	
    private void Start()
    {
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

    // Handles mouse rotation for adjusting racoons forward direction
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
    
    // When racoon dead, shows black/white overlay
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
