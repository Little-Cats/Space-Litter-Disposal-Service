using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    #region fields
    public InputMaster controls;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float force = 10f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    #endregion

    #region properties
    public Vector2 MoveInput { get => moveInput;}
    #endregion

    #region monobehaviour
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Movement.performed += context => Move(context.ReadValue<Vector2>()); // adding Move() trigger with wasdKeys inputs
        controls.Player.Movement.canceled += context => Move(context.ReadValue<Vector2>()); // adding Move() trigger with wasdKeys inputs
        controls.Player.Shoot.performed += context => Shoot(); // adding Shoot() trigger with spacebar input
        controls.Player.Shoot.canceled += context => Shoot(); // adding Shoot() trigger with spacebar input

        rb = GetComponent<Rigidbody2D>(); // retrieve rigidbody so that we can move the player
    }

    private void FixedUpdate()
    {
        // movement
        //rb.velocity = moveInput * speed; // option A
        rb.AddForce(moveInput * force); // option B
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    #endregion

    #region methods
    void Move(Vector2 direction)
    {
        moveInput = direction; 
    }

    void Shoot()
    {
        //Debug.Log("You're trying to shoot!");
    }
    #endregion
}
