using UnityEngine;

public class HumanAI : MonoBehaviour
{
    public float moveSpeed = 15f;                  // 이동 속도
    public float detectionRange = 10f;             // UFO 인식 범위 (멀리서도 반응)
    public Vector2 areaMin = new Vector2(-64.5f, 18.4f); // 왼쪽·아래 경계
    public Vector2 areaMax = new Vector2(145f, 64f);     // 오른쪽·위 경계

    private Vector3 moveDirection;
    private Transform ufo;

    private float directionChangeTimer = 0f;
    public float directionChangeInterval = 1.5f;   // UFO 멀 때 랜덤 방향 바꾸는 주기

    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ufo = GameObject.FindWithTag("UFO")?.transform;
        PickRandomDirection();
    }

    void Update()
    {
        if (ufo == null) return;

        Vector3 toUFO = ufo.position - transform.position;

        if (toUFO.magnitude < detectionRange)
        {
            // UFO가 가까우면 무조건 반대 방향
            moveDirection = (-toUFO).normalized;
        }
        else
        {
            // UFO가 멀면 일정 주기로 방향 변경
            directionChangeTimer += Time.deltaTime;
            if (directionChangeTimer >= directionChangeInterval)
            {
                PickRandomDirection();
                directionChangeTimer = 0f;
            }
        }

        // 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 경계 체크 후 반사 이동
        if (transform.position.x < areaMin.x || transform.position.x > areaMax.x)
        {
            moveDirection.x *= -1;
            ClampPosition();
        }
        if (transform.position.z < areaMin.y || transform.position.z > areaMax.y)
        {
            moveDirection.z *= -1;
            ClampPosition();
        }

        // 애니메이션 방향 세팅
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.z))
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveZ", 0f);
        }
        else
        {
            animator.SetFloat("MoveX", 0f);
            animator.SetFloat("MoveZ", moveDirection.z);
        }

        animator.SetBool("isMoving", moveDirection.magnitude > 0.01f);
    }

    void PickRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, areaMin.x, areaMax.x);
        float clampedZ = Mathf.Clamp(transform.position.z, areaMin.y, areaMax.y);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
