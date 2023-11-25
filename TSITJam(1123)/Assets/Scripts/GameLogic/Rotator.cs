using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public static Action RotationStart;
    public static Action RotationEnd;
    public void RotateLeft()
    {
        RotationStart();
        transform.DORotate(transform.eulerAngles + new Vector3(0, 0, 90),
                           DesignSettings.Instance.RotationTime)
        .OnComplete(() => { RotationEnd(); });
    }
    public void RotateRight()
    {
        RotationStart();
        transform.DORotate(transform.eulerAngles + new Vector3(0, 0, -90),
                           DesignSettings.Instance.RotationTime)
        .OnComplete(() => { RotationEnd(); });
    }
}
