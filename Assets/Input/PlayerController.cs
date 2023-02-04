using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speed_multiplier = 0.05f;
    [SerializeField]private float flight_impulse_magnitude = 4.0f;
    private InputAction move_input;
    private InputAction cursor_move_input;
    private InputAction clicking_input;
    private Rigidbody2D rb2d;

    private int jump_count = 0;
    [SerializeField]private int max_jump_count = 10;
    
    
    // Animator - Luryann
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        move_input = InputManager.Instance.my_input_actions.ActionMap.PlayerMovement;
        cursor_move_input = InputManager.Instance.my_input_actions.ActionMap.CursorMovement;
        clicking_input = InputManager.Instance.my_input_actions.ActionMap.Click;
        InputManager.Instance.my_input_actions.ActionMap.Fly.started += fly;
    }

    private void FixedUpdate()
    {
        move();
        dig();
    }

    private void move()
    {
        // Walk animation
        _animator.SetFloat("Moving", math.abs(move_input.ReadValue<Vector2>().x));
        
        // Flip sprites
        if (math.abs(move_input.ReadValue<Vector2>().x) > 0 )
        {
            _spriteRenderer.flipX = move_input.ReadValue<Vector2>().x < 0;
        }
        
        
        if (move_input.ReadValue<Vector2>().x != 0)
        {
            rb2d.transform.Translate(new Vector2(move_input.ReadValue<Vector2>().x, 0.0f) * speed_multiplier);
        }
        
        /*
        if (move_input.ReadValue<Vector2>().x != 0)
        {
            Vector2 move_vector = new Vector2(move_input.ReadValue<Vector2>().x * 10, 0.0f);
            rb2d.AddForce(move_vector, ForceMode2D.Impulse);
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, 5);
        }
        else
        {
            float new_Horizontal_velocity = Mathf.Lerp(rb2d.velocity.x, 0, 0.5f);
            rb2d.velocity = new Vector2 (new_Horizontal_velocity, rb2d.velocity.y);
        }
        */
    }

    private void fly(InputAction.CallbackContext context)
    {
        /*
        float percentage_difference = 1;
        
        if (rb2d.velocity.y > 0) // if going up cap speed on axis
        {
            float delta_velocity = rb2d.velocity.y - max_fly_velocity;
                
            percentage_difference -= (delta_velocity / max_fly_velocity);
            Debug.Log(percentage_difference);
        }
        */
        _animator.SetBool("Flying", true);
        if (jump_count < max_jump_count)
        {
            rb2d.AddForce(Vector2.up * flight_impulse_magnitude, ForceMode2D.Impulse);
            jump_count++;
        }
    }

    private void dig()
    {
        _animator.SetBool("Mining_Pickaxe", clicking_input.ReadValue<Single>().Equals(1));
    }

    private void OnCollisionEnter2D()
    {
        _animator.SetBool("Flying", false);
        // need to check if other collider belongs to floor.
        jump_count = 0;
    }
}
