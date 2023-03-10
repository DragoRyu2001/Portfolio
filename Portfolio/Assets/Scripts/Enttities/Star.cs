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
    public void SetDetails(StarDetails details, Star parent = null)
    {
        if (parent != null)
        {
            this.parent = parent;
            this.layer = parent.layer + 1;
            canMove = true;
            transform.localScale = Vector3.one * .5f;

        }
        else
        {

            this.layer = 1;
        }
        radius = .5f / (layer);
        transform.localScale = new Vector3(.5f, .5f, .5f);
        this.name = details.name;
        this.details = details;
        Material mat = GetComponent<MeshRenderer>().material;
        mat.SetColor("_EmissionColor", details.starColor);
        GetComponent<MeshRenderer>().material = mat;
        currentAngle = Time.fixedDeltaTime * GameManager.Instance.rotateSpeed * Random.Range(.5f, 2f);
        pathParent.gameObject.SetActive(false);
        GetChildrenDetails();
    }
    private void GetChildrenDetails()
    {
        if (details.subCategories.Count == 0) return;
        if (parent != null)
            radiusSegment = parent.radiusSegment / (details.subCategories.Count * 2);
        else
            radiusSegment = GameManager.Instance.radius;
        Debug.Log(this.name + "'s yMax: " + radiusSegment);
        float x = 2 * radiusSegment;

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
        pathObj.transform.localScale = Vector3.one * (4 * radius)*layer;
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
    public void ShowPath(bool show)
    {
        pathParent.gameObject.SetActive(show);
    }

}
