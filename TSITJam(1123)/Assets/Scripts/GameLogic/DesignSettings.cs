using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignSettings : MonoBehaviour
{
    static public DesignSettings Instance;
    public float MoveSpeed;
    public float AccelerationFactor;
    public float DeccelerationFactor;
    public float RotationTime;

    public float FootScanRadius;
    public LayerMask LayersToStay;

    public float RotationTimeRange;
    private void Start(){
        if(Instance is null){
            Instance = this;
        }
    }
}
