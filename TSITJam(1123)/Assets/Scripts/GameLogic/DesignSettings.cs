using UnityEngine;

public class DesignSettings : MonoBehaviour
{
    static public DesignSettings Instance;
    public float MoveSpeed;
    public float AccelerationFactor;
    public float DeccelerationFactor;
    [Range(-2,0)] public float GravityFactor;
    public float RotationTime;

    public LayerMask LayersToStay;

    public float RotationTimeRange;
    public Vector3 ItemsLocalPosition;
    public float PickUpDistance;
    public LayerMask ItemsLayer;
    public float MinPickupAngle;
    public float MaxPickupAngle;
    public float DialogueWindowAppearingTime;
    private void Start()
    {
        if (Instance is null)
        {
            Instance = this;
        }
    }
}
