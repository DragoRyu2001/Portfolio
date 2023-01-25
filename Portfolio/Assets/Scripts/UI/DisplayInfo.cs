using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI dateTimeText;
    [SerializeField] Image titleImage;
    [SerializeField] DisplayButton displayButton;

    public void ShowDetails(StarDetails itemDetails)
    {
        CleanUp();
        if(itemDetails.title!="")
        {
            titleText.text = itemDetails.title;
            titleText.gameObject.SetActive(true);
        }
        if(itemDetails.titleImage!=null)
        {
            titleImage.gameObject.SetActive(true);
            titleImage.sprite = itemDetails.titleImage;
            titleImage.preserveAspect = true;
        }
        if(itemDetails.context!="")
        {
            descriptionText.gameObject.SetActive(true);
            descriptionText.text = itemDetails.context;
        }
        if(itemDetails.date.year!=0)
        {
            dateTimeText.gameObject.SetActive(true);
            dateTimeText.text = itemDetails.date.GetDateTimeText();
        }
        if(itemDetails.links.Count>0)
        {
            displayButton.gameObject.SetActive(true);
            displayButton.CreateButtons(itemDetails.links);
        }
    }
    public void ClosePopUp()
    {
        CleanUp();
        GameManager.Instance.ResetCamera();
        gameObject.SetActive(false);
    }
    private void CleanUp()
    {
        titleText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        dateTimeText.gameObject.SetActive(false);
        titleImage.gameObject.SetActive(false);
        displayButton.CleanUp();
    }
}
