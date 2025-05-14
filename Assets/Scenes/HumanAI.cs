using UnityEngine;

public class HumanAI : MonoBehaviour
{
    public float moveSpeed = 15f;                  // 이동 속도
    public float detectionRange = 3f;               // UFO 인식 범위
    public Vector2 areaMin = new Vector2(-5f, -3f); // 이동 가능한 XZ 경계 (좌하단)
    public Vector2 areaMax = new Vector2(5f, 2f);   // 이동 가능한 XZ 경계 (우상단)

    private Vector3 moveDirection;
    private Transform ufo;

    private float directionChangeTimer = 0f;
    public float directionChangeInterval = 1.5f;    // 방향 바꾸는 주기 (초)

    void Start()
    {
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
