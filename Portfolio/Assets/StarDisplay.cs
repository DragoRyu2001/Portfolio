using TMPro;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class StarDisplay : MonoBehaviour
{
    private Transform _camera;
    private TextMeshProUGUI _displayText;
    #region UnityEvents
    private void OnEnable()
    {
        GameManager.Instance.HideAllDisplays += HideInfo;
    }

    private void OnDisable()
    {
        GameManager.Instance.HideAllDisplays -= HideInfo;
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        _displayText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        Debug.Assert(Camera.main != null, "Camera.main != null");

        _camera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(_camera.position * -1);
    }

    #endregion
    public void SetTitle(string title)
    {
        if(_displayText==null)
            Init();
        _displayText.text = title;
    }

    public void ShowInfo()
    {
        if (_displayText == null)
            Init();
        _displayText.gameObject.SetActive(true);
    }
    private void HideInfo()
    {
        if (_displayText == null)
            Init();
        _displayText.gameObject.SetActive(false);
    }
}
