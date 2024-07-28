using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField]
    float Mass = 5f;

    public float speed = 5f;

    [SerializeField]
    float _GravityForce = -9.81f;

    [SerializeField]
    bool _isgrounded;

    [SerializeField]
    Transform GroundCheckTransform;

    [SerializeField]
    float raycastLength;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float yVelocity;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    KeyCode JumpKey;

    [SerializeField]
    float CrouchSpeed = 10f;

    [SerializeField]
    float TargetSpeedMultiplier = 5f;

    [SerializeField]
    GunScript gunScript;

    float BaseCamFov;
    float CameraFov;

    float BaseTargetSpeedMultiplier;

    float horizontal;
    float vertical;

    public bool playerIsMoving = false;

    float Height;

    float BaseSpeed;

    [SerializeField] bool isRunning;

    [SerializeField] bool isCrouching;

    [SerializeField] float TargetSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Height = characterController.height;
        BaseSpeed = speed;
        BaseTargetSpeedMultiplier = TargetSpeedMultiplier;
        CameraFov = Camera.main.fieldOfView;
        BaseCamFov = Camera.main.fieldOfView;
    }

    void Update()
    {

        IsMoving();

        GroundCheck();

        Gravity();

        HandleMovement();

        if (Input.GetKeyDown(JumpKey))
        {
            Jump();
        }

        speed = Mathf.Lerp(speed, TargetSpeed, TargetSpeedMultiplier * Time.deltaTime);
        

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && IsMoving())
        {
            //gunScript.CanAim = false;
            TargetSpeed = BaseSpeed * 1.5f;
            TargetSpeedMultiplier = BaseTargetSpeedMultiplier / 4;
           // Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, CameraFov + 5, 1f * Time.deltaTime);
            isRunning = true;
        }
        else if (!isCrouching)
        {
            //gunScript.CanAim = true;
            TargetSpeed = BaseSpeed;
            TargetSpeedMultiplier = BaseTargetSpeedMultiplier / 2;
            //if (!gunScript.IsAiming)
              //  Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, BaseCamFov, 0.25f * Time.deltaTime);
            isRunning = false;
        }

        

        if (Input.GetKey(KeyCode.LeftControl) && !isRunning)
        {
            characterController.height = Mathf.Lerp(characterController.height, Height / 2, CrouchSpeed * Time.deltaTime);
            //speed = Mathf.Lerp(speed, BaseSpeed / 2, CrouchSpeed * Time.deltaTime);
            TargetSpeed = BaseSpeed / 2;
            TargetSpeedMultiplier = BaseTargetSpeedMultiplier / 2;
            isCrouching = true;
        }
        else if (!isRunning)
        {
            characterController.height = Mathf.Lerp(characterController.height, Height, CrouchSpeed / 2 * Time.deltaTime);
            //speed = Mathf.Lerp(speed, BaseSpeed, CrouchSpeed / 2 * Time.deltaTime);
            TargetSpeedMultiplier = BaseTargetSpeedMultiplier;
            TargetSpeed = BaseSpeed;
            if (characterController.height >= 1.9f)
            {
                isCrouching = false;
            }
            else if (characterController.height <= 1.9f)
            {
                isCrouching = true;
            }
        }
    }

    void HandleMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = Vector3.ClampMagnitude(movement, 1f);

        Vector3 moveDirection = transform.TransformDirection(movement);
        characterController.Move(speed * Time.deltaTime * moveDirection);
    }

    void Gravity()
    {
        if (!_isgrounded)
        {
            yVelocity += (_GravityForce * Mass) * Time.deltaTime;
        }
        else
        {
            yVelocity += _GravityForce * Time.deltaTime;
            if (yVelocity < _GravityForce)
            {
                yVelocity = _GravityForce;
            }
        }

        Vector3 velocity = new Vector3(0, yVelocity, 0);
        characterController.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (_isgrounded)
        {
            yVelocity = jumpHeight /*Mathf.Sqrt(jumpHeight * -2f * _GravityForce)*/;
        }
    }

    void GroundCheck()
    {
        _isgrounded = Physics.Raycast(GroundCheckTransform.position, Vector3.down, raycastLength, groundLayer);
    }

    bool IsMoving()
    {
        if (horizontal != 0f || vertical != 00)
        {
            return playerIsMoving = true;
        }
        else
        {
            return playerIsMoving = false;
        }
    }

    public bool IsMovingRight()
    {
        if (horizontal >= 0.01f)
            return true;
        else
            return false;
    }
}