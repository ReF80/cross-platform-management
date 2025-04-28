using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    [Header("Mobile Controls")]
    [SerializeField] private Joystick mobileJoystick;
    
    private Rigidbody2D _rb;
    private IPlayerControlState _currentState;
    private float _targetRotation;
    private float _currentRotationVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        InitializeControlState();
    }

    private void InitializeControlState()
    {
        bool isMobile = SystemInfo.deviceType == DeviceType.Handheld;
        
        if (isMobile && mobileJoystick != null)
        {
            _currentState = new MobileControlState(mobileJoystick);
            Debug.Log("Mobile controls initialized");
        }
        else
        {
            _currentState = new PCControlState();
            Debug.Log("PC controls initialized");
        }
    }

    private void Update()
    {
        _currentState.HandleInput(this);
        _currentState.UpdateState(this);
        ApplyRotation();
    }

    public void ProcessMovement(Vector2 input)
    {
        // Движение
        Vector2 movement = input.normalized * moveSpeed;
        _rb.linearVelocity = movement;
        
        // Определение направления поворота
        if (input.magnitude > 0.1f)
        {
            _targetRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        }
    }

    private void ApplyRotation()
    {
        // Плавный поворот с демпфированием
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.SmoothDampAngle(
            currentAngle, 
            _targetRotation, 
            ref _currentRotationVelocity, 
            Time.deltaTime * rotationSpeed);
            
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    public void SwitchToPCControls()
    {
        _currentState = new PCControlState();
        if (mobileJoystick != null) mobileJoystick.gameObject.SetActive(false);
    }

    public void SwitchToMobileControls()
    {
        if (mobileJoystick != null)
        {
            _currentState = new MobileControlState(mobileJoystick);
            mobileJoystick.gameObject.SetActive(true);
        }
    }
}