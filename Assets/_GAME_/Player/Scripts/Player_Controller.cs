using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class Player_Controller : MonoBehaviour
{
    #region Editor Data
    [Header("Movement Attributes")]
    [SerializeField] public float _moveSpeed = 10f;
    [SerializeField] public float _collisionOffset = 0.05f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] ContactFilter2D _movementFilter;
    #endregion

    #region Internal Data
    private Vector2 _moveDir = Vector2.zero;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    #endregion


    #region Start Logic
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    #endregion


    // For repeated tick functions 
    #region Tick
    private void FixedUpdate()
    {
        MovementUpdate();
    }
    #endregion

    #region Input Logic
    private void OnMove(InputValue movementValue)
    {
        _moveDir = movementValue.Get<Vector2>();
    }
    #endregion

    #region Movement Logic
    private void MovementUpdate()
    {
        // If movement input is not 0, try to move
        if (_moveDir != Vector2.zero)
        {
            // Check for potential collisions
            int count = _rb.Cast(
                _moveDir, // X and Y values between 1 and -1 that represents direction
                _movementFilter, // The settings that determine where a collision can occur on, such as layers to collide with
                _castCollisions, // List of collisions to store the found collisions into after the cast is finished
                _moveSpeed * Time.fixedDeltaTime + _collisionOffset // The amount to cast equal to the movement plus an offset
            );

            // If no collision, move
            if (count == 0)
            {
                Vector2 move = _moveDir * _moveSpeed * Time.fixedDeltaTime;
                _rb.MovePosition(_rb.position + move);
            }
        }
    }
    #endregion
}
