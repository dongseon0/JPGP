using UnityEngine;

public class HumanAI : MonoBehaviour
{
    public float moveSpeed = 15f;                  // 이동 속도
    public float detectionRange = 3f;               // UFO 인식 범위
    public Vector2 areaMin = new Vector2(-64.5f, 18.4f); // ( 왼쪽 경계 , 아래쪽 경계 )
    public Vector2 areaMax = new Vector2(145f, 64f);   // ( 오른쪽 경계 , 위쪽 경계 )

    private Vector3 moveDirection;
    private Transform ufo;

    private float directionChangeTimer = 0f;
    public float directionChangeInterval = 1.5f;    // 방향 바꾸는 주기 (초)

    // 애니메이션 제어 추가
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // 애니메이터 연결
        ufo = GameObject.FindWithTag("UFO")?.transform;
        PickRandomDirection();
    }


    void Update()
    {
        if (ufo == null) return;

        Vector3 toUFO = ufo.position - transform.position;

        if (toUFO.magnitude < detectionRange)
        {
            // 빔이 가까워지면 반대 방향으로 도망
            moveDirection = (-toUFO).normalized;
        }
        else
        {
            // 일정 시간마다 방향 변경
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

        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveZ", moveDirection.z);
        animator.SetBool("isMoving", moveDirection.magnitude > 0.1f);

    }

    void PickRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
    }

    void ClampPosition()
    {
        // 경계 밖으로 안 나가게 고정
        float clampedX = Mathf.Clamp(transform.position.x, areaMin.x, areaMax.x);
        float clampedZ = Mathf.Clamp(transform.position.z, areaMin.y, areaMax.y);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
