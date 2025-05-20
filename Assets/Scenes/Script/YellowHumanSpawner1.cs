using UnityEngine;

public class YellowHumanSpawner : MonoBehaviour
{
    public GameObject yellowHumanPrefab;     // Black 인간 프리팹
    public float spawnInterval = 2f;        // 몇 초마다 생성할지
    public int maxCount = 5;                // 최대 몇 명까지 존재할지


    public Vector2 areaMin = new Vector2(-64.5f, 18.4f); // ( 왼쪽 경계 , 아래쪽 경계 )
    public Vector2 areaMax = new Vector2(145f, 64f);   // ( 오른쪽 경계 , 위쪽 경계 )

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // 현재 존재하는 Black 인간 수 확인
            int currentCount = CountHumansByType(HumanType.Black);

            if (currentCount < maxCount)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(areaMin.x, areaMax.x),
                    transform.position.y,
                    Random.Range(areaMin.y, areaMax.y)
                );

                Instantiate(yellowHumanPrefab, spawnPos, yellowHumanPrefab.transform.rotation);

            }
        }
    }

    int CountHumansByType(HumanType type)
    {
        int count = 0;
        foreach (Human h in FindObjectsOfType<Human>())
        {
            if (h.type == type && !h.isCaptured)
                count++;
        }
        return count;
    }
}
