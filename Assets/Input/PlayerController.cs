using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    
    Vector3 mousePosition, targetPosition;
    private float speed_multiplier = 0.05f;
    private float flight_impulse_magnitude = 4.0f;
    private InputAction move_input;
    private InputAction cursor_move_input;
    private InputAction clicking_input;
    private Rigidbody2D rb2d;
    public float click_timer = 0;

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject block;

    private int jump_count = 0;
    private int max_jump_count = 3;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        move_input = InputManager.Instance.my_input_actions.ActionMap.PlayerMovement;
        cursor_move_input = InputManager.Instance.my_input_actions.ActionMap.CursorMovement;
        clicking_input = InputManager.Instance.my_input_actions.ActionMap.Click;
        InputManager.Instance.my_input_actions.ActionMap.Fly.started += fly;
    }

    private void Update()
    {
        dig();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
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

        if (jump_count < max_jump_count)
        {
            rb2d.AddForce(Vector2.up * flight_impulse_magnitude, ForceMode2D.Impulse);
            jump_count++;
        }
    }

    private void dig()
    {
        Debug.Log("Running");
        if (clicking_input.ReadValue<Single>() == 1)
        {
            Debug.Log("mouse down");
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            if (hit.collider != null)
            {
                click_timer += 1.0f * Time.deltaTime;
                if (click_timer >= 2)
                {
                    if (hit.collider.gameObject == block) Destroy(block);
                    Debug.Log("HIT");
                    Debug.Log(click_timer);
                    click_timer = 0;
                    
                }
            }
        }

        if (clicking_input.ReadValue<Single>() == 0)
        {
            click_timer = 0;
        }
    }

    private void OnCollisionEnter2D()
    {
        // need to check if other collider belongs to floor.
        jump_count = 0;
    }
}
