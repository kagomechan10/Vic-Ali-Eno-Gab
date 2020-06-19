using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    
    
    private Inputs inputs;
    private Rigidbody2D myRB;
    private Vector2 direction;
    
    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;
        inputs.Player.Jump.performed += OnJumpperformed;
    }

    private void OnJumpperformed(InputAction.CallbackContext obj)
    {
        myRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    
    
    
    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
    }

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        var myRigidBody = GetComponent<Rigidbody2D>();
        direction.y = 0;
        if (myRigidBody.velocity.sqrMagnitude < maxSpeed)
            myRigidBody.AddForce(direction * speed);
    }
}
