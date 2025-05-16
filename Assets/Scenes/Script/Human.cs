using UnityEngine;

public enum HumanType { Black, Yellow, Dog }

public class Human : MonoBehaviour
{
    public HumanType type = HumanType.Black;   // Inspector에서 종류 선택
    public int scoreValue = 10;                // 포획 시 점수
    public float moveSpeed = 3f;               // AI 이동 속도

    [HideInInspector] public float captureTimer = 0f;
    [HideInInspector] public bool isCaptured = false;

    void Start()
    {
        isCaptured = false;
        captureTimer = 0f;

        switch (type)
        {
            case HumanType.Black:
                moveSpeed = 3f;
                scoreValue = 10;
                break;
            case HumanType.Yellow:
                moveSpeed = 5f;
                scoreValue = 20;
                break;
            case HumanType.Dog:
                moveSpeed = 7f;
                scoreValue = 30;
                break;
        }
    }
}
