using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UFOController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A, D
        float moveZ = Input.GetAxisRaw("Vertical");   // W, S
        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
   
    }
}
