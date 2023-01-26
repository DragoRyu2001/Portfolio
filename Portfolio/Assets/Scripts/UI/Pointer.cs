using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    private Image select;
    private Color selectColor;
    private float selectOpacity;

    [SerializeField] private float speed;
    [SerializeField] private Camera cam;

    private RectTransform rectTransform;

    private bool isSelectUI;
    private enum PointerState
    {
        Deselect,
        Select
    }
    private PointerState state;
    private void Start()
    {
        select = transform.GetChild(0).GetComponent<Image>();
        selectColor = select.color;

        cam= Camera.main;

        rectTransform= GetComponent<RectTransform>();

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
        switch(state)
        {
            case PointerState.Deselect:
                DeSelectLogic();
                break;
            case PointerState.Select:
                SelectLogic();
                break;
        }
        UpdateImage();
        
    }
    #region UpdateFunctions
    private void SetState()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(cam.transform.position,ray.direction, out RaycastHit hit,  5000f))
        {
            if (hit.transform.CompareTag("Star"))
                state = PointerState.Select;
            else
                state = PointerState.Deselect;
        }
        else
        {
            state = PointerState.Deselect;
        }
    }
    private void UpdatePosition()
    {
        rectTransform.position = Input.mousePosition;
    }

    private void SelectLogic()
    {
        if (selectOpacity >= 1)
        {
            selectOpacity = 1;
            return;
        }
        selectOpacity+= Time.deltaTime*speed;
    }
    private void DeSelectLogic()
    {
        if (selectOpacity <= 0)
        {
            selectOpacity = 0;
            return;
        }
        selectOpacity -= Time.deltaTime*speed;
    }
    private void UpdateImage()
    {
        selectColor.a = selectOpacity;
        select.color = selectColor;
    }
    #endregion
}
