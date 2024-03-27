using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [FormerlySerializedAs("targetGroup")] [SerializeField]
    private CinemachineTargetGroup TargetGroup;
    public void SetFocus(Transform focus)
    {
        if(TargetGroup==null)
            TargetGroup= GetComponent<CinemachineTargetGroup>();
        TargetGroup.m_Targets = null;
        TargetGroup.AddMember(focus, 1f, 1f);
        foreach(Transform child in focus)
        {
            TargetGroup.AddMember(child, 1f, 1f);
        }
    }

}
