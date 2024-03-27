using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Entities
{
    public class Star : MonoBehaviour
    {
        public StarDetails Details { get; private set; }

        [SerializeField] private Transform PathParent;
        [SerializeField] private StarDisplay Display;

        private bool _canMove;
        private int _layer;
        private bool _glow;
        private float _radiusSegment;
        private float _glowValue;
        private float _currentAngle;

        private readonly List<Star> _stars = new();
        private Star _parent;
        private GameObject _pathObjectPrefab;
        private SphereCollider _sphereCollider;
        public void SetDetails(StarDetails details, Star parent = null)
        {
            if (parent != null)
            {
                _parent = parent;
                _layer = parent._layer + 1;
                _canMove = true;
                transform.localScale = Vector3.one * (1/(Mathf.PI));
            }
            else
            {
                _layer = 1;
            }

            name = details.name;
            Details = details;
            Display.SetTitle(Details.Title);
            gameObject.AddComponent<StarColour>();
            _sphereCollider = GetComponent<SphereCollider>();
            _currentAngle = Time.fixedDeltaTime * GameManager.Instance.RotateSpeed * Random.Range(.5f, 2f);
            PathParent.gameObject.SetActive(false);
            GetChildrenDetails();
            GameManager.Instance.DisableAllStarColliders += DisableCollider;
        }
        public void ShowPath(bool show)
        {
            PathParent.gameObject.SetActive(show);
        }

        private void GetChildrenDetails()
        {
            if (Details.SubCategories.Count == 0) return;
            if (_parent != null)
                _radiusSegment = _parent._radiusSegment / (Details.SubCategories.Count * Mathf.PI);
            else
                _radiusSegment = GameManager.Instance.Radius;
            Debug.Log(name + "'s yMax: " + _radiusSegment);

            for (int i = 0; i < Details.SubCategories.Count; i++)
            {
                float radius = (i + 1) * _radiusSegment;
                Transform starTransform;
                _stars.Add(Instantiate(GameManager.Instance.StarPrefab, (starTransform = transform).position.GetPolarDistance(radius, Random.Range(0f, 2 * Mathf.PI)), Quaternion.identity, starTransform).GetComponent<Star>());
                SetChildrenPath(radius);
            }
            for (int i = 0; i < _stars.Count; i++)
            {
                _stars[i].SetDetails(Details.SubCategories[i], this);
            }
        }
        private void SetChildrenPath(float radius)
        {
            if (_pathObjectPrefab == null)
                _pathObjectPrefab = GameManager.Instance.PathObject;
            GameObject pathObj = Instantiate(_pathObjectPrefab, transform.position, _pathObjectPrefab.transform.rotation, PathParent);
            pathObj.transform.localScale = Mathf.PI* radius * _layer * Vector3.one;
        }
        private void Update()
        {
            if (!_canMove) return;

            SetPosition();
        }


        private void SetPosition()
        {
            transform.RotateAround(_parent.transform.position, Vector3.up, _currentAngle);
        }

        private void OnMouseDown()
        {
            GameManager.Instance.DisplayInformation(this, transform);
        }
        
        public void ShowChildrenDisplay()
        {
            foreach (Star star in _stars)
            {
                star.ShowDisplay();                
            }
        }

        public void ShowDisplay()
        {
            Display.ShowInfo();
        }

        public void EnableColliderInChildren()
        {
            foreach (Star star in _stars)
            {
                star.EnableCollider();
            }
        }

        public void EnableCollider()
        {
            _sphereCollider.enabled = true;
        }

        private void DisableCollider()
        {
            _sphereCollider.enabled = false;
        }
    }
}
