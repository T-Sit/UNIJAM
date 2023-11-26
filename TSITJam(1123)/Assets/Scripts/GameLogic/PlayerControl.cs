using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayerControl : Freezable
{
    private float _lastLevelRotation;
    [SerializeField] private Rotator rotator;
    private Vector3 _baseEu = new(0, 0, 0);
    [SerializeField] private Transform _footPoint;
    private Vector3 _rightRot = new(0, 0, 0);
    private Vector3 _leftRot = new(0, 180, 0);
    private float _yScalingVelocity;
    private PlayerItemController _playerItemController;

    private const float c_CheckNotZeroVelocity = 0.1f;
    private const float c_GroundPredictionDelay = 0.2f;
    private float _groundPredictionTiming;
    private EventInstance _playerWalk;

    override protected void Awake()
    {
        base.Awake();
        _playerItemController = GetComponent<PlayerItemController>();
        _playerWalk = AudioManager.Instance.CreateEventInstance(FMODEvents.Instance.FootSteps);
    }
    void Update()
    {
        DoMovement();
        DoPlayerRotation();
        DoPickUpCheck();
        DoLevelRotation();
        PredictGroundFall();
        UpdateSound();
    }

    private void DoPickUpCheck()
    {
        if (!_isFreezed && IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerItemController.ParsePickDropButton();
            }
        }

    }

    override protected void ReleaseTime()
    {
        base.ReleaseTime();
        transform.SetParent(null);
    }
    override protected void FreezeTime()
    {
        base.FreezeTime();
        transform.SetParent(rotator.transform);
    }


    private void DoLevelRotation()
    {
        if ((IsGrounded
            && !_isFreezed)
            || (Time.time <= _lastLevelRotation  + DesignSettings.Instance.RotationTime+ DesignSettings.Instance.RotationTimeRange/2
            && Time.time >= _lastLevelRotation + DesignSettings.Instance.RotationTime-+ DesignSettings.Instance.RotationTimeRange/2))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                _lastLevelRotation = Time.time;
                rotator.RotateLeft();
            }
            else if (Input.GetKey(KeyCode.E))
            {
                _lastLevelRotation = Time.time;
                rotator.RotateRight();
            }
        }
        else
        {
            transform.eulerAngles = _baseEu;
        }
    }

    private void DoPlayerRotation()
    {
        if (!_isFreezed && IsGrounded)
        {
            float x = Input.GetAxisRaw("Horizontal");
            if (x > 0)
            {
                transform.eulerAngles = _rightRot;
            }
            else if (x < 0)
            {
                transform.eulerAngles = _leftRot;
            }
        }
    }

    private void DoMovement()
    {
        if (!_isFreezed)
        {
            if (IsGrounded)
            {
                _yScalingVelocity = 0;
                Vector3 movement = CalculateMovement();
                _rb.AddForce(movement, ForceMode.Force);
            }
            else
            {
                float yVelocity = DesignSettings.Instance.GravityFactor;
                _yScalingVelocity += yVelocity;
                _rb.velocity = new Vector3(0, _yScalingVelocity, 0);
            }
        }
    }

    private bool IsGrounded => Physics.OverlapSphere(_footPoint.position,
                                                     DesignSettings.Instance.FootScanRadius,
                                                     DesignSettings.Instance.LayersToStay).Length != 0;

    private Vector3 CalculateMovement()
    {
        float movement = Input.GetAxisRaw("Horizontal") * DesignSettings.Instance.MoveSpeed;
        float speedDif = movement - _rb.velocity.x;
        float accelFactor = Mathf.Abs(movement) > 1e-3f ? DesignSettings.Instance.AccelerationFactor : DesignSettings.Instance.DeccelerationFactor;
        return accelFactor * speedDif * Vector3.right;
    }

    private void UpdateSound()
    {
        if (_isFreezed)
        {
            StopFootsteps();
            return;
        }

        if (Mathf.Abs(_rb.velocity.x) >= c_CheckNotZeroVelocity && IsGrounded)
        {
            PLAYBACK_STATE playbackState;
            _playerWalk.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                float randomPitchValue = Random.Range(-15, 15);
                _playerWalk.setParameterByName("RandomPitch", randomPitchValue);
                _playerWalk.start();
            }
        }
        else
        {
            StopFootsteps();
        }
    }

    public void StopFootsteps() 
        => _playerWalk.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    private void PredictGroundFall()
    {
        if (_rb.velocity.y < -c_CheckNotZeroVelocity && IsGrounded && (Time.time - _groundPredictionTiming >= c_GroundPredictionDelay))
        {
            _groundPredictionTiming = Time.time;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.PlayerFall);
        } 
    }

}
