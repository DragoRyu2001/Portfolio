using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class DisplayButton : MonoBehaviour
{
    public Button buttonPrefab;
    public void CreateButtons(List<CustomLinks> links)
    {
        foreach(CustomLinks link in links)
        {
            Button button = Instantiate(buttonPrefab, this.transform);
            button.onClick.AddListener(() => GoToLink(link.url));
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = link.title;
        }
    }
    private void GoToLink(string url)
    {
        if (url == null || url == "") return;
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
