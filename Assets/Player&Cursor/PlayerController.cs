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
    Vector3 mousePosition, targetPosition;
    public GameObject cursor_object;
    [SerializeField]private float speed_multiplier = 0.05f;
    [SerializeField]private float flight_impulse_magnitude = 4.0f;
    private InputAction move_input;
    private InputAction cursor_move_input;
    private InputAction clicking_input;
    private Rigidbody2D rb2d;

    public float click_timer = 0;

    [SerializeField] private Camera cam;
    //[SerializeField] private GameObject block;
    
    private int jump_count = 0;
    [SerializeField]private int max_jump_count = 10;
    
    // Animator - Luryann
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public GameObject hit_ground;

    private void Awake()
    {
        resetCursor();

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
        InputManager.Instance.my_input_actions.ActionMap.SnapCursor.started += snapCursor;
    }

    private void Update()
    {
        moveCursor();
        dig_act();
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
        
        // Move player
        if (move_input.ReadValue<Vector2>().x != 0)
        {
            rb2d.transform.Translate(new Vector2(move_input.ReadValue<Vector2>().x, 0.0f) * speed_multiplier);
        }
    }

    private void moveCursor()
    {
        Vector3 potential_new_pos = cursor_object.transform.position + new Vector3((cursor_move_input.ReadValue<Vector2>().x /100), (cursor_move_input.ReadValue<Vector2>().y/100), 0.0f);
        Vector3 world_space_pos = cam.WorldToViewportPoint(potential_new_pos);
        
        if (world_space_pos.x > 0.0f && world_space_pos.x < 1.0f)
        {
            cursor_object.transform.position = new Vector3(potential_new_pos.x, cursor_object.transform.position.y, cursor_object.transform.position.z);
        }

        if (world_space_pos.y > 0.0f && world_space_pos.y < 1.0f)
        {
            cursor_object.transform.position = new Vector3(cursor_object.transform.position.x, potential_new_pos.y, cursor_object.transform.position.z);
        }
    }

    private void fly(InputAction.CallbackContext context)
    {
        _animator.SetBool("Flying", true);
        if (jump_count < max_jump_count)
        {
            rb2d.AddForce(Vector2.up * flight_impulse_magnitude, ForceMode2D.Impulse);
            jump_count++;
        }
    }

    private void snapCursor(InputAction.CallbackContext context)
    { 
        resetCursor();
    }

    private void resetCursor()
    {
        cursor_object.transform.position = transform.position;
        cursor_object.transform.position = new Vector3(cursor_object.transform.position.x, cursor_object.transform.position.y, -1.0f);
    }

    private void dig()
    {
        _animator.SetBool("Mining_Pickaxe", clicking_input.ReadValue<Single>().Equals(1));
    }

    private void dig_act()
    {
        if (clicking_input.ReadValue<Single>() == 1)
        {
            //RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Ray ray = new Ray(cursor_object.transform.position, Vector3.forward);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.name);
                //Debug.Log(click_timer);
                click_timer += 1.0f * Time.deltaTime;
                
                if (click_timer >= 2)
                {
                    /*
                    if (hit.collider.gameObject == block)
                    {
                        Destroy(block);
                    }
                    */

                    if (hit.collider.gameObject.tag == "Block")
                    {
                        Destroy(hit.collider.gameObject);
                    }

                    click_timer = 0;
                }
            }
        }

        if (clicking_input.ReadValue<Single>() == 0)
        {
            click_timer = 0;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        _animator.SetBool("Flying", false);
        
        // Test collision
        spawnDustOnCollision(col);
        
        // NEED TO CHECK WHETHER COLLIDING OBJECT IS FLOOR.
        jump_count = 0;
    }
    void spawnDustOnCollision(Collision2D col)
    {
        foreach (ContactPoint2D contact in col.contacts)
        {
            Vector2 hitPoint = contact.point;
            Instantiate(hit_ground, new Vector3(hitPoint.x, hitPoint.y - 0.07f, -0.1f), Quaternion.identity);
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            resetCursor();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
