using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public Star parent;
    public StarDetails details;
    public int layer;
    public float radiusSegment;

    private List<Star> stars = new List<Star>();
    private float currentAngle = 0f;
    private float radius;
    
    [SerializeField]
    private Transform pathParent;
    [SerializeField] 
    private bool canMove = false;
    private GameObject pathObjectPrefab;

    private bool _glow;
    private float _glowValue;

    public void SetDetails(StarDetails details, Star parent = null)
    {
        if (parent != null)
        {
            this.parent = parent;
            this.layer = parent.layer + 1;
            canMove = true;
            transform.localScale = Vector3.one * (1/(Mathf.PI));
        }
        else
        {
            this.layer = 1;
        }
        radius = (1/Mathf.PI) / (layer);
        this.name = details.name;
        this.details = details;

        gameObject.AddComponent<StarColour>();

        currentAngle = Time.fixedDeltaTime * GameManager.Instance.rotateSpeed * Random.Range(.5f, 2f);
        pathParent.gameObject.SetActive(false);
        GetChildrenDetails();
    }
    public void ShowPath(bool show)
    {
        pathParent.gameObject.SetActive(show);
    }

    private void GetChildrenDetails()
    {
        if (details.subCategories.Count == 0) return;
        if (parent != null)
            radiusSegment = parent.radiusSegment / (details.subCategories.Count * Mathf.PI);
        else
            radiusSegment = GameManager.Instance.radius;
        Debug.Log(this.name + "'s yMax: " + radiusSegment);

        for (int i = 0; i < details.subCategories.Count; i++)
        {
            float radius = (i + 1) * radiusSegment;
            stars.Add(Instantiate(GameManager.Instance.starPrefab, transform.position.GetPolarDistance(radius, Random.Range(0f, 2 * Mathf.PI)), Quaternion.identity, this.transform).GetComponent<Star>());
            SetChildrenPath(radius);
        }
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].SetDetails(details.subCategories[i], this);
        }
    }
    private void SetChildrenPath(float radius)
    {
        if (this.pathObjectPrefab == null)
            pathObjectPrefab = GameManager.Instance.PathObject;
        GameObject pathObj = Instantiate(pathObjectPrefab, transform.position, pathObjectPrefab.transform.rotation, pathParent);
        pathObj.transform.localScale = Mathf.PI* radius * layer * Vector3.one;
    }
    private void Update()
    {
        if (!canMove) return;

        SetPosition();
    }


    private void SetPosition()
    {
        transform.RotateAround(parent.transform.position, Vector3.up, currentAngle);
    }

    private void OnMouseDown()
    {
        GameManager.Instance.DisplayInformation(this, transform);
    }
}
