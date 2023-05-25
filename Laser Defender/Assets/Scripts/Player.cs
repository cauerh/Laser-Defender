using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector2 rawInput;
    [SerializeField] float moveSpeed = 10f;


    [SerializeField] float paddinLeft;
    [SerializeField] float paddinRight;
    [SerializeField] float paddinBotoom;
    [SerializeField] float paddinTop;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
    }

    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddinLeft, maxBounds.x - paddinRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddinBotoom, maxBounds.y - paddinTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed; 
        }
    }
}
