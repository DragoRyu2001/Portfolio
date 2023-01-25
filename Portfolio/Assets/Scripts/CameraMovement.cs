using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private CinemachineTargetGroup targetGroup;
    public void SetFocus(Transform focus)
    {
        if(targetGroup==null)
            targetGroup= GetComponent<CinemachineTargetGroup>();
        targetGroup.m_Targets = null;
        targetGroup.AddMember(focus, 1f, 1f);
        foreach(Transform child in focus)
        {
            targetGroup.AddMember(child, 1f, 1f);
        }
    }

}
