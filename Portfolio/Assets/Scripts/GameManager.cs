using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Star mainStar;
    private Star currentStar;
    public static GameManager Instance { get; private set; }

    [SerializeField] private StarDetails startStar;
    [SerializeField] private DisplayInfo displayInfo;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private float startStarSize;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        FindRadius();
        StartSequence();
    }
    private void StartSequence()
    {
        mainStar = Instantiate(starPrefab, Vector3.zero, Quaternion.identity);
        mainStar.SetDetails(startStar);
        mainStar.transform.localScale = Vector3.one * startStarSize;
        mainStar.transform.GetChild(0).localScale = Vector3.one * 0.64f;
        cameraMovement.SetFocus(mainStar.transform);
        SetCurrentStar(mainStar);
    }

    private void SetCurrentStar(Star star)
    {
        if(currentStar!= null)
            currentStar.ShowPath(false);
        currentStar = star;
        currentStar.ShowPath(true);
    }

    //Common Settings
    public Star starPrefab;
    public float rotateSpeed = 0.5f;
    public float radius = 1f;
    public GameObject PathObject;
    [SerializeField]
    private float radiusSegment;

    private void FindRadius()
    {
        float maxY = RecursiveRadius(radiusSegment, 1, startStar);
        radius = maxY;
    }
    private float RecursiveRadius(float minY, int levelDepth, StarDetails myDetails)
    {
        bool isGrandParent = false;
        float maxY = 0;
        foreach (StarDetails star in myDetails.subCategories)
        {
            if (star.subCategories.Count == 0) continue;
            float childRadius = RecursiveRadius(minY, levelDepth + 1, star);
            if ( childRadius> maxY)
            {
                isGrandParent = true;
                maxY = childRadius;
            }
        }
        if (!isGrandParent)
        {
            float y = (1/Mathf.PI) / (float)(levelDepth);
            maxY = 2 * ((myDetails.subCategories.Count) * y);
        }
        return maxY;
    }
    public void DisplayInformation(Star star, Transform transform)
    {
        SetCurrentStar(star);
        displayInfo.gameObject.SetActive(true);
        displayInfo.ShowDetails(star.details);
        cameraMovement.SetFocus(transform);
        if(star.details.name=="AboutMe")
            AudioManager.Instance.StarClick();
    }
    public void ResetCamera()
    {
        SetCurrentStar(mainStar);
        cameraMovement.SetFocus(mainStar.transform);
    }

}
