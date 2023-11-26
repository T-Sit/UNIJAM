using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public static Action RotationStart;
    public static Action RotationEnd;
    private Vector3 _rotation;
    public void RotateLeft()
    {
        RotationStart();
        _rotation.z += 90;
        ApplyRotation()
        .OnComplete(() => { RotationEnd(); });
    }
    public void RotateRight()
    {
        RotationStart();
        _rotation.z -= 90;
        ApplyRotation().OnComplete(() => { RotationEnd(); });
    }

    private TweenerCore<Quaternion, Vector3, QuaternionOptions> ApplyRotation()
    {
        transform.DOKill();
        return transform.DORotate(_rotation,
                                   DesignSettings.Instance.RotationTime);
    }
}
