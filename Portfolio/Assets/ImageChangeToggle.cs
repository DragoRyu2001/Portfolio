using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ImageChangeToggle : MonoBehaviour
{
    [FormerlySerializedAs("deselectedImage")] [FormerlySerializedAs("_deselectedImage")] [SerializeField]
    private Sprite DeselectedImage;
    [FormerlySerializedAs("selectedImage")] [FormerlySerializedAs("_selectedImage")] [SerializeField]
    private Sprite SelectedImage;
    [FormerlySerializedAs("onClick")] [FormerlySerializedAs("_onClick")] [SerializeField]
    private UnityEvent<bool> OnClick;
    private Image _image;
    private bool _isOn = false;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeImage);
    }
    private void ChangeImage()
    {
        _isOn = !_isOn;
        if(_image == null)
            _image = GetComponent<Image>();
        _image.sprite = _isOn ? SelectedImage : DeselectedImage;
        OnClick.Invoke(_isOn);
    }
}
