using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    #region fields
    public InputMaster controls;
    [SerializeField]
    private float speed;
    #endregion

    #region monobehaviour
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Movement.performed += context => Move(context.ReadValue<Vector2>()); // adding Move() trigger with wasdKeys inputs
        controls.Player.Shoot.performed += context => Shoot();
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
        Debug.Log("You're trying to move: " + direction);   
    }

    void Shoot()
    {
        Debug.Log("You're trying to shoot!");
    }
    #endregion
}
