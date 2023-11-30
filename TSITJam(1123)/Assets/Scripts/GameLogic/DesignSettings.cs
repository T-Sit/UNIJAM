using UnityEngine;

public class DesignSettings : MonoBehaviour
{
    static public DesignSettings Instance;
    public float MoveSpeed;
    public float AccelerationFactor;
    public float DeccelerationFactor;
    public float Gravity;
    public float timeToApplyGravity;
    public float RotationTime;

    public LayerMask LayersToStay;

    public float RotationTimeRange;
    public Vector3 ItemsLocalPosition;
    public float PickUpDistance;
    public LayerMask ItemsLayer;
    public float MinPickupAngle;
    public float MaxPickupAngle;
    public float DialogueWindowAppearingTime;
    public float ButtonClickedScale;
    public float ButtonClickScalingTime;

    public LayerMask LayersToBounce;

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
    }
}
