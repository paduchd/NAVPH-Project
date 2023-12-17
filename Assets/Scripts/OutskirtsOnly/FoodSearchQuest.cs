using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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

    private void ContainerSearched()
    {
        searched += 1;

        if (searched < totalSearchPlaces)
        {
            message.text = "Nothing found!\n" + searched + "/" + totalSearchPlaces + " searched";
        }
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

    private void InSearchRange()
    {
        uiInteractionText.gameObject.SetActive(true);
    }

    private void OutOfSearchRange()
    {
        uiInteractionText.gameObject.SetActive(false);
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(3);
        message.text = "";
    }
}
