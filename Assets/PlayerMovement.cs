using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    public Stats stats;
    public bool allowMovement = true;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3,3);
        Physics2D.IgnoreLayerCollision(3,7);
        mainCamera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(allowMovement){
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*stats.movementSpeed;      
        }
    }
}
