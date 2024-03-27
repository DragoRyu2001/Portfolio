using System;
using DragoRyu.Utilities;
using Entities;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using DisplayInfo = UI.DisplayInfo;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Action HideAllDisplays;
    public Action DisableAllStarColliders;
    
    [FormerlySerializedAs("startStar")]       [SerializeField] private StarDetails StartStar;
    [FormerlySerializedAs("displayInfo")]     [SerializeField] private DisplayInfo DisplayInfo;
    [FormerlySerializedAs("cameraMovement")]  [SerializeField] private CameraMovement CameraMovement;
    [FormerlySerializedAs("startStarSize")]   [SerializeField] private float StartStarSize;
    
    private Star _mainStar;
    private Star _currentStar;
    
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
        _mainStar = Instantiate(StarPrefab, Vector3.zero, Quaternion.identity);
        _mainStar.SetDetails(StartStar);
        _mainStar.transform.localScale = Vector3.one * StartStarSize;
        
        Transform mainStarTransform;
        
        (mainStarTransform = _mainStar.transform).GetChild(0).localScale = Vector3.one * 0.64f;
        CameraMovement.SetFocus(mainStarTransform);
        
        
        SetCurrentStar(_mainStar);
        
        _mainStar.ShowDisplay();    
    }

    private void SetCurrentStar(Star star)
    {
        if(_currentStar!= null)
            _currentStar.ShowPath(false);
        _currentStar = star;
        _currentStar.ShowPath(true);
        if(star.Details.SubCategories.Count>0)
            HideAllDisplays.SafeInvoke();
        _currentStar.ShowChildrenDisplay();
    }

    //Common Settings
    [FormerlySerializedAs("starPrefab")] public Star StarPrefab;
    [FormerlySerializedAs("rotateSpeed")] public float RotateSpeed = 0.5f;
    [FormerlySerializedAs("radius")] public float Radius = 1f;
    [FormerlySerializedAs("pathObject")] public GameObject PathObject;

    private void FindRadius()
    {
        float maxY = RecursiveRadius(1, StartStar);
        Radius = maxY;
    }
    private float RecursiveRadius(int levelDepth, StarDetails myDetails)
    {
        bool isGrandParent = false;
        float maxY = 0;
        foreach (StarDetails star in myDetails.SubCategories)
        {
            if (star.SubCategories.Count == 0) continue;
            float childRadius = RecursiveRadius(levelDepth + 1, star);
            if ( childRadius> maxY)
            {
                isGrandParent = true;
                maxY = childRadius;
            }
        }
        if (!isGrandParent)
        {
            float y = (1/Mathf.PI) / levelDepth;
            maxY = 2 * ((myDetails.SubCategories.Count) * y);
        }
        return maxY;
    }
    public void DisplayInformation(Star star, Transform starTransform)
    {
        SetCurrentStar(star);
        
        DisplayInfo.gameObject.SetActive(true);
        DisplayInfo.ShowDetails(star.Details);
        if (star.Details.SubCategories.Count > 0)
        {
            DisableAllStarColliders.SafeInvoke();
        }
        star.EnableColliderInChildren();
        
        CameraMovement.SetFocus(starTransform);
        AudioManager.Instance.StarClick();
    }
    public void ResetCamera()
    {
        SetCurrentStar(_mainStar);
        DisableAllStarColliders.SafeInvoke();
        _mainStar.EnableColliderInChildren();
        _mainStar.EnableCollider();
        _mainStar.ShowDisplay();
        CameraMovement.SetFocus(_mainStar.transform);
    }


    
}
