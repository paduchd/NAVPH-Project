using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodSearchQuest : MonoBehaviour
{
    public TextMeshProUGUI message;
    public TextMeshProUGUI UIInteractionText;
    
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
            message.text = "You found a fish!";
        }

        UIInteractionText.gameObject.SetActive(false);
        StartCoroutine(DisableText());
    }

    private void InSearchRange()
    {
        UIInteractionText.gameObject.SetActive(true);
    }

    private void OutOfSearchRange()
    {
        UIInteractionText.gameObject.SetActive(false);
    }

    private IEnumerator DisableText()
    {
        yield return new WaitForSeconds(3);
        message.text = "";
    }
}
