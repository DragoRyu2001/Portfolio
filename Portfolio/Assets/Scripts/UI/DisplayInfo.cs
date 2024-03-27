using System;
using DragoRyu.Utilities;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class DisplayInfo : MonoBehaviour
    {
        public Action ClosePopupAction;
        
        [FormerlySerializedAs("titleText")] [SerializeField] TextMeshProUGUI TitleText;
        [FormerlySerializedAs("descriptionText")] [SerializeField] TextMeshProUGUI DescriptionText;
        [FormerlySerializedAs("dateTimeText")] [SerializeField] TextMeshProUGUI DateTimeText;
        [FormerlySerializedAs("titleImage")] [SerializeField] Image TitleImage;
        [FormerlySerializedAs("displayButton")] [SerializeField] DisplayButton DisplayButton;
        
        
        public void ShowDetails(StarDetails itemDetails)
        {
            CleanUp();
            if(itemDetails.Title!=string.Empty)
            {
                TitleText.text = itemDetails.Title;
                TitleText.gameObject.SetActive(true);
            }
            if(itemDetails.TitleImage!=null)
            {
                TitleImage.gameObject.SetActive(true);
                TitleImage.sprite = itemDetails.TitleImage;
                TitleImage.preserveAspect = true;
            }
            if(itemDetails.Context!=string.Empty)
            {
                DescriptionText.gameObject.SetActive(true);
                DescriptionText.text = itemDetails.Context;
            }
            if(itemDetails.Date.Year!=0)
            {
                DateTimeText.gameObject.SetActive(true);
                DateTimeText.text = itemDetails.Date.GetDateTimeText();
            }

            if (itemDetails.Links.Count <= 0) return;
            DisplayButton.gameObject.SetActive(true);
            DisplayButton.CreateButtons(itemDetails.Links);
        }
        public void ClosePopUp()
        {
            CleanUp();
            GameManager.Instance.ResetCamera();
            gameObject.SetActive(false);
            ClosePopupAction.SafeInvoke();
        }
        private void CleanUp()
        {
            TitleText.gameObject.SetActive(false);
            DescriptionText.gameObject.SetActive(false);
            DateTimeText.gameObject.SetActive(false);
            TitleImage.gameObject.SetActive(false);
            DisplayButton.CleanUp();
        }
    }
}
