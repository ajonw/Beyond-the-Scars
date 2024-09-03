using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class Player_Controller : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField] public float _moveSpeed = 4f;
    [SerializeField] public float _collisionOffset = 0.05f;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] ContactFilter2D _movementFilter;
    [SerializeField] Animator _animator;

    public LayerMask interactablesLayer;


    private PlayerInput playerInput;
    private Vector2 _moveDir = Vector2.zero;
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();




    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void EnableControls()
    {
        playerInput.enabled = true;
    }
    public void DisableControls()
    {
        playerInput.enabled = false;
    }

    private void FixedUpdate()
    {
        MovementUpdate(_moveDir);
    }

    private void OnMove(InputValue movementValue)
    {
        _moveDir = movementValue.Get<Vector2>();
    }


    public void MovementUpdate(Vector2 _moveDir)
    {
        // If movement input is not 0, try to move
        if (_moveDir != Vector2.zero)
        {
            bool move_successful = TryMove(_moveDir);

            if (!move_successful)
            {
                move_successful = TryMove(new Vector2(_moveDir.x, 0));
                if (!move_successful)
                {
                    move_successful = TryMove(new Vector2(0, _moveDir.y));
                }
            }

            // If diagonal movement face player to left/right
            if (_moveDir.x != 0 && _moveDir.y != 0)
            {
                _animator.SetFloat("X", _moveDir.x);
                _animator.SetFloat("Y", 0);
            }
            // only set when an input move is provided otherwise keep original facing direction
            else if (_moveDir.x != 0 || _moveDir.y != 0)
            {
                _animator.SetFloat("X", _moveDir.x);
                _animator.SetFloat("Y", _moveDir.y);
            }

            // set isMoving true when moving
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Check for potential collisions
        int count = _rb.Cast(
            direction, // X and Y values between 1 and -1 that represents direction
            _movementFilter, // The settings that determine where a collision can occur on, such as layers to collide with
            _castCollisions, // List of collisions to store the found collisions into after the cast is finished
            _moveSpeed * Time.fixedDeltaTime + _collisionOffset // The amount to cast equal to the movement plus an offset
        );

        // If no collision, move
        if (count == 0)
        {
            Vector2 move = direction * _moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + move);
            return true;
        }
        else
        {
            return false;
        }
    }
}
