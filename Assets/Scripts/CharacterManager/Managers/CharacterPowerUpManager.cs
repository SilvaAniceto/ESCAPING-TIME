using UnityEngine;
using UnityEngine.Events;

public class CharacterPowerUpManager : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private bool _hasInfinityDash;
    [Header("Air Jump")]
    [SerializeField] private bool _hasInfinityAirJump;
    [Header("Wall Move")]
    [SerializeField] private bool _hasInfinityWallMove;

    private bool _hasTemporaryDash;
    private float _temporaryDashTime;

    private bool _hasTemporaryAirJump;
    private float _temporaryAirJumpTime;

    private bool _hasTemporaryWallMove;
    private float _temporaryWallMoveTime;

    private bool _dashOnCoolDown;
    private bool _dashIsWaitingGroundedState;

    private bool _airJumpIsAllowed;

    [HideInInspector] public UnityEvent OnPowerUpInteractableRecharge = new UnityEvent();
    [HideInInspector] public UnityEvent<string, float, CharacterPowerUpManager> OnDashPowerStateChange = new UnityEvent<string, float, CharacterPowerUpManager>();
    [HideInInspector] public UnityEvent<string, float, CharacterPowerUpManager> OnAirJumpPowerStateChange = new UnityEvent<string, float, CharacterPowerUpManager>();
    [HideInInspector] public UnityEvent<string, float, CharacterPowerUpManager> OnWallMovePowerStateChange = new UnityEvent<string, float, CharacterPowerUpManager>();

    public bool HasInfinityDash
    {
        get => _hasInfinityDash;
        set
        {
            if (_hasInfinityDash == value)
            {
                return;
            }

            _hasInfinityDash = value;

            ServiceLocator.UIManager.SetAirJumpPowerUpUI(_hasInfinityDash);
        }
    }
    public bool HasTemporaryDash
    {
        get => _hasTemporaryDash;
        set
        {
            if (value == _hasTemporaryDash || HasInfinityDash)
            {
                return;
            }

            _hasTemporaryDash = value;

            string clip = _hasTemporaryDash ? "PwrUp_UI_Lit" : "PwrUp_UI_Unlit";
            float coolDown = _hasTemporaryDash ? _temporaryDashTime : 0f;

            OnDashPowerStateChange.Invoke(clip, coolDown, this);
        }
    }
    public bool HasDash => HasTemporaryDash || HasInfinityDash;
    public bool DashIsAllowed => HasDash && !_dashOnCoolDown && !_dashIsWaitingGroundedState;

    public bool HasInfinityAirJump
    {
        get => _hasInfinityAirJump;
        set
        {
            if (_hasInfinityAirJump == value)
            {
                return;
            }

            _hasInfinityAirJump = value;

            ServiceLocator.UIManager.SetAirJumpPowerUpUI(_hasInfinityAirJump);
        }
    }
    public bool HasTemporaryAirJump
    {
        get => _hasTemporaryAirJump;
        set
        {
            if (value == _hasTemporaryAirJump || HasInfinityAirJump)
            {
                return;
            }

            _hasTemporaryAirJump = value;

            string clip = _hasTemporaryAirJump ? "PwrUp_UI_Lit" : "PwrUp_UI_Unlit";
            float coolDown = _hasTemporaryAirJump ? _temporaryAirJumpTime : 0f;

            OnAirJumpPowerStateChange.Invoke(clip, coolDown, this);
        }
    }
    public bool HasAirJump => HasTemporaryAirJump || HasInfinityAirJump;
    public bool AirJumpIsAllowed => HasAirJump && _airJumpIsAllowed;

    public bool HasInfinityWallMove
    {
        get => _hasInfinityWallMove;
        set
        {
            if (_hasInfinityWallMove == value)
            {
                return;
            }

            _hasInfinityWallMove = value;

            ServiceLocator.UIManager.SetAirJumpPowerUpUI(_hasInfinityWallMove);
        }
    }
    public bool HasTemporaryWallMove
    {
        get => _hasTemporaryWallMove;
        set
        {
            if (value == _hasTemporaryWallMove || HasInfinityWallMove)
            {
                return;
            }

            _hasTemporaryWallMove = value;

            string clip = _hasTemporaryWallMove ? "PwrUp_UI_Lit" : "PwrUp_UI_Unlit";
            float coolDown = _hasTemporaryWallMove ? _temporaryWallMoveTime : 0f;

            OnWallMovePowerStateChange.Invoke(clip, coolDown, this);
        }
    }
    public bool HasWallMove => HasInfinityWallMove || HasTemporaryWallMove;

    public void SetDashCooldown()
    {
        _dashOnCoolDown = true;
    }
    public void SetDashWaitingForGround()
    {
        _dashIsWaitingGroundedState = true;
    }
    public void ResetDashOnLand()
    {
        _dashIsWaitingGroundedState = false;
    }
    public void ResetDashCoolDown()
    {
        _dashOnCoolDown = false;
    }
    public void SetTemporaryDash(float coolDown = 0)
    {
        _temporaryDashTime = coolDown;

        HasTemporaryDash = true;
        if (coolDown > 0)
        {
            OnPowerUpInteractableRecharge.AddListener(() =>
            {
                HasTemporaryDash = false;
            });
        }
    }
    public void RegisterDashCallback()
    {
        OnDashPowerStateChange.AddListener(ServiceLocator.UIManager.SetOvertimeDashPowerUpUI);
    }

    public void EnableAirJump()
    {
        _airJumpIsAllowed = true;
    }
    public void DisableAirJump()
    {
        _airJumpIsAllowed = false;
    }
    public void SetTemporaryAirJump(float coolDown = 0)
    {
        _temporaryAirJumpTime = coolDown;

        HasTemporaryAirJump = true;
        if (coolDown > 0)
        {
            OnPowerUpInteractableRecharge.AddListener(() =>
            {
                HasTemporaryAirJump = false;
            });
        }
    }
    public void RegisterAirJumpCallback()
    {
        OnAirJumpPowerStateChange.AddListener(ServiceLocator.UIManager.SetOvertimeAirJumpPowerUpUI);
    }

    public void SetTemporaryWallMove(float coolDown = 0)
    {
        _temporaryWallMoveTime = coolDown;

        HasTemporaryWallMove = true;
        if (coolDown > 0)
        {
            OnPowerUpInteractableRecharge.AddListener(() =>
            {
                HasTemporaryWallMove = false;
            });
        }
    }
    public void RegisterWallMoveCallback()
    {
        OnWallMovePowerStateChange.AddListener(ServiceLocator.UIManager.SetOvertimeWallMovePowerUpUI);
    }

    void OnDestroy()
    {
        OnDashPowerStateChange.RemoveAllListeners();
        OnAirJumpPowerStateChange.RemoveAllListeners();
        OnWallMovePowerStateChange.RemoveAllListeners();
        OnPowerUpInteractableRecharge.RemoveAllListeners();
    }

    public void DispatchPowerUpInteractableRecharge()
    {
        OnPowerUpInteractableRecharge?.Invoke();
        OnPowerUpInteractableRecharge.RemoveAllListeners();
    }
}
