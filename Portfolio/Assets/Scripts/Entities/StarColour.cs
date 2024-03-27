using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Star))]
    public class StarColour : MonoBehaviour
    {
        private const string PrimaryColor = "_Primary";
        private const float MinIntensity = 0.5f;
        private const float MaxIntensity = 2f;
        private const float SelectSpeed = 10f;
        private const float DeselectSpeed = 0.5f;

        private Star _star;
        private bool _hover;
        private float _colorIntensity;

        private MaterialPropertyBlock _mpb;
        private MeshRenderer _meshRenderer;
        private static readonly int Primary = Shader.PropertyToID(PrimaryColor);


        private void Awake()
        {
            _mpb = new MaterialPropertyBlock();
            _star = GetComponent<Star>();
            _meshRenderer = GetComponent<MeshRenderer>();
            SetColor(_star.Details.StarColor, MinIntensity);
            _colorIntensity = MaxIntensity;
        }

        private void Update()
        {
            if (Application.isMobilePlatform)
            {
                return;
            }
            switch (_hover)
            {
                case true when _colorIntensity < MaxIntensity:
                    _colorIntensity += Time.deltaTime * SelectSpeed;
                    SetColor(_star.Details.StarColor, _colorIntensity);
                    break;
                case false when _colorIntensity > MinIntensity:
                    _colorIntensity -= Time.deltaTime * DeselectSpeed;
                    SetColor(_star.Details.StarColor, _colorIntensity);
                    break;
            }
        }

        private void SetColor(Color color, float intensity)
        {
            _mpb.SetColor(Primary, color * intensity);
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
}