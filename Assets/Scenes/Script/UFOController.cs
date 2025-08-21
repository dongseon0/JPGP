using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UFOController : MonoBehaviour
{
    public float moveSpeed = 5f;


    public Vector2 areaMin = new Vector2(-64.5f, 50f);   // X 최소, Z 최소
    public Vector2 areaMax = new Vector2(145f, 100f);    // X 최대, Z 최대

    public float margin = 0f; // 벽에서 약간 띄우고 싶으면 0.5f 같은 값

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;

        Vector3 target = rb.position + move * moveSpeed * Time.fixedDeltaTime;

        // 경계 클램프 (XZ만)
        float minX = areaMin.x + margin;
        float maxX = areaMax.x - margin;
        float minZ = areaMin.y + margin;
        float maxZ = areaMax.y - margin;

        target.x = Mathf.Clamp(target.x, minX, maxX);
        target.z = Mathf.Clamp(target.z, minZ, maxZ);

        rb.MovePosition(target);
    }
}
