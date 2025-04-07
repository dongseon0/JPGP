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
        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized; //normalized로 이동속도가 일정하게 유지되도록 함
    
        // rb.MovePosition(...) -> 부드럽고 충돌 가능하게 이동시켜주는 함수, 벽에 부딪혀도 충돌 작동
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
   
    }
}
