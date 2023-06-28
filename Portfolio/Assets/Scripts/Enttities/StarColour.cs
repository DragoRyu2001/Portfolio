using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Star))]
public class StarColour : MonoBehaviour
{
    private const string PRIMARY_COLOR = "_Primary";
    private const float MIN_INTENSITY = 0.5f;
    private const float MAX_INTENSITY = 2f;
    private const float SPEED = 10f;

    private Star _star;
    private bool _hover = false;
    private float _colorIntensity;

    private MaterialPropertyBlock _mpb;
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        _mpb = new MaterialPropertyBlock();
        _star = GetComponent<Star>();
        _meshRenderer = GetComponent<MeshRenderer>();
        SetColor(_star.details.starColor, MIN_INTENSITY);
    }

    private void Update()
    {
        if(_hover && _colorIntensity<MAX_INTENSITY)
        {
            _colorIntensity += Time.deltaTime * SPEED;
            SetColor(_star.details.starColor, _colorIntensity);
        }
        else if(!_hover && _colorIntensity>MIN_INTENSITY)
        {
            _colorIntensity -= Time.deltaTime * SPEED;
            SetColor(_star.details.starColor, _colorIntensity);
        }
    }

    private void SetColor(Color color, float intensity)
    {
        _mpb.SetColor(PRIMARY_COLOR, color * intensity);
        _meshRenderer.SetPropertyBlock(_mpb);
    }
    private void OnMouseEnter()
    {
        _hover = true;
        AudioManager.Instance.PlanetNote();
    }
    private void OnMouseExit()
    {
        _hover = false;
    }
}
