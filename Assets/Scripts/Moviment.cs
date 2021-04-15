using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moviment : MonoBehaviour
{
    Controls ctrls;

    public GameObject cameraPlayer;
    public float speed, sprintSpeed;
    public float mouseSen, controlerSen;
    Rigidbody rb;

    Vector2 lookupRotation, inputRotation;
    Vector3 moveAmount;
    Vector3 moveDir;
    bool isSprint, lookingByControl, movingUp, movingDown;

    private void Awake()
    {
        ctrls = new Controls();

        ctrls.Player.Sprint.performed += ctx => isSprint = true;
        ctrls.Player.Sprint.canceled += ctx => isSprint = false;

        ctrls.Player.Move.performed += ctx => Movimentation(ctx);
        ctrls.Player.Move.canceled += ctx => Movimentation(ctx);

        ctrls.Player.Look.performed += ctx => Look(ctx);
        ctrls.Player.Look.canceled += ctx => inputRotation = Vector2.zero;

        ctrls.Player.Up.performed += ctx => movingUp = true;
        ctrls.Player.Up.canceled += ctx => movingUp = false;
        ctrls.Player.Down.performed += ctx => movingDown = true;
        ctrls.Player.Down.canceled += ctx => movingDown = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSprint = false;
        lookingByControl = false;
    }

    public void Movimentation(InputAction.CallbackContext ctx)
    {
        Vector2 value = ctx.ReadValue<Vector2>();
        moveDir = new Vector3(value.y, 0, value.x).normalized;
    }
    public void Look(InputAction.CallbackContext ctx)
    {   
        inputRotation = ctx.ReadValue<Vector2>();

        if (ctx.control.device.displayName.Equals("Mouse"))
        {
            lookingByControl = false;
            RotateCamera(inputRotation, mouseSen);
        }
        else
        {
            lookingByControl = true;
        }
    }

    private void RotateCamera(Vector2 input, float sensitivity)
    {
        lookupRotation.x += input.x *= sensitivity / 100;

        lookupRotation.y += input.y * sensitivity / 100;
        lookupRotation.y = Mathf.Clamp(lookupRotation.y, -90f, 90f);

        transform.localEulerAngles = new Vector3(-lookupRotation.y, lookupRotation.x, transform.rotation.z);
    }


    void Update()
    {
        if (lookingByControl)
        {
            RotateCamera(inputRotation, controlerSen);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveForward = transform.forward * moveDir.x * 10 * Time.fixedDeltaTime;
        Vector3 moveSide = transform.right * moveDir.z * 10 * Time.fixedDeltaTime;

        int verticalmove = 0;
        if (movingUp) verticalmove = 1;
        else if (movingDown) verticalmove = -1;
        else verticalmove = 0;
        Vector3 moveVertical = transform.up * verticalmove * 10 * Time.fixedDeltaTime;

        rb.velocity = (moveForward + moveSide + moveVertical) * (isSprint ? sprintSpeed : speed);
    }

    private void OnEnable()
    {
        ctrls.Enable();
    }
    private void OnDisable()
    {
        ctrls.Disable();
    }
}
