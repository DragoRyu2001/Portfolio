using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageChangeToggle : MonoBehaviour
{
    [SerializeField]
    private Sprite _deselectedImage;
    [SerializeField]
    private Sprite _selectedImage;
    [SerializeField]
    private UnityEvent<bool> _onClick;
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
        _image.sprite = _isOn ? _selectedImage : _deselectedImage;
        _onClick.Invoke(_isOn);
    }
}
