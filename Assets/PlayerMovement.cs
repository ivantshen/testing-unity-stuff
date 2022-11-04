using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    public Rigidbody2D rb;
    public Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*stats.movementSpeed;  
    }
}
