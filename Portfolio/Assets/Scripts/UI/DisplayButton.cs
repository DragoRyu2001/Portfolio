using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class DisplayButton : MonoBehaviour
    {
        [FormerlySerializedAs("buttonPrefab")] public Button ButtonPrefab;
        public void CreateButtons(List<CustomLinks> links)
        {
            foreach(var link in links)
            {
                var button = Instantiate(ButtonPrefab, this.transform);
                button.onClick.AddListener(() => GoToLink(link.URL));
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = link.Title;
            }
        }
        private static void GoToLink(string url)
        {
            if (string.IsNullOrEmpty(url)) return;
            Application.OpenURL(url);
        }
        public void CleanUp()
        {
            foreach(Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
