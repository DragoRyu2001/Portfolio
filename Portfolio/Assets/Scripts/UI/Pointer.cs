using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class Pointer : MonoBehaviour
    {
        private Image _select;
        private Color _selectColor;
        private float _selectOpacity;

        [FormerlySerializedAs("speed")] [SerializeField] private float Speed;
        [FormerlySerializedAs("cam")] [SerializeField] private Camera Cam;

        private RectTransform _rectTransform;

        private bool _isSelectUI;
        private enum PointerState
        {
            DESELECT,
            SELECT
        }
        private PointerState _state;
        private void Start()
        {
            _select = transform.GetChild(0).GetComponent<Image>();
            _selectColor = _select.color;

            Cam ??= Camera.main;

            _rectTransform= GetComponent<RectTransform>();

            Cursor.visible = false;
        }
        private void OnApplicationFocus(bool focus)
        {
            if(focus)
                Cursor.visible = false;
        }
        void Update()
        {
            UpdatePosition();
            SetState();
            switch(_state)
            {
                case PointerState.DESELECT:
                    DeSelectLogic();
                    break;
                case PointerState.SELECT:
                    SelectLogic();
                    break;
            }
            UpdateImage();
        
        }
        #region UpdateFunctions
        private void SetState()
        {
            if (Cam == null)
                Cam = Camera.main;
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(Cam.transform.position,ray.direction, out RaycastHit hit,  5000f))
            {
                if (hit.transform.CompareTag("Star"))
                    _state = PointerState.SELECT;
                else
                    _state = PointerState.DESELECT;
            }
            else
            {
                _state = PointerState.DESELECT;
            }
        }
        private void UpdatePosition()
        {
            _rectTransform.position = Input.mousePosition;
        }

        private void SelectLogic()
        {
            if (_selectOpacity >= 1)
            {
                _selectOpacity = 1;
                return;
            }
            _selectOpacity+= Time.deltaTime*Speed;
        }
        private void DeSelectLogic()
        {
            if (_selectOpacity <= 0)
            {
                _selectOpacity = 0;
                return;
            }
            _selectOpacity -= Time.deltaTime*Speed;
        }
        private void UpdateImage()
        {
            _selectColor.a = _selectOpacity;
            _select.color = _selectColor;
        }
        #endregion
    }
}
