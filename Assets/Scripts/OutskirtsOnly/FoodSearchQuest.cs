using System.Collections;
using TMPro;
using UnityEngine;

// Class for the mini quest on outskirts - searching 5 containers
public class FoodSearchQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI uiInteractionText;
    [SerializeField] private PlayerStamina playerStamina;
    [Range(0, 100)] [SerializeField] private float staminaReplenishAmount = 10f;
    
    private int totalSearchPlaces = 5;
    private int searched = 0;

    private void OnEnable()
    {
        SearchContainer.OnContainerSearch += ContainerSearched;
        SearchContainer.OnSearchRange += InSearchRange;
        SearchContainer.OnLeaveSearchRange += OutOfSearchRange;
    }
    
    private void OnDisable()
    {
        SearchContainer.OnContainerSearch -= ContainerSearched;
        SearchContainer.OnSearchRange -= InSearchRange;
        SearchContainer.OnLeaveSearchRange -= OutOfSearchRange;
    }

    // When a container is searched print a message
    private void ContainerSearched()
    {
        searched += 1;
        
        // If the player didn't search all containers
        if (searched < totalSearchPlaces)
        {
            message.text = "Nothing found!\n" + searched + "/" + totalSearchPlaces + " searched";
        }
        // Give the player food on the last container
        else
        {
            PlayerEventManager.TriggerOnFoodEaten(true);
            ObjectiveManager.Instance.UpdateTaskObjectiveProgress();
            playerStamina.IncreaseMax(staminaReplenishAmount);
            message.text = "You found a fish!";
        }

        uiInteractionText.gameObject.SetActive(false);
        StartCoroutine(DisableText());
    }
    
    // Checks if the player is in interaction range
    private void InSearchRange()
    {
        uiInteractionText.gameObject.SetActive(true);
    }
    
    // Checks if the player leaves interaction range
    private void OutOfSearchRange()
    {
        uiInteractionText.gameObject.SetActive(false);
    }

    // Disables the message after 3 seconds
    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(3);
        message.text = "";
    }
}
