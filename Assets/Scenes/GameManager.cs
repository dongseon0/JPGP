using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int count = 0;
    public float timer = 0f;

    public TMP_Text scoreText;
    public TMP_Text countText;
    public TMP_Text timerText;

    private void Awake()
    {
        // 싱글턴 설정 (다른 씬에서도 유지되게 하고 싶으면 DontDestroyOnLoad 추가)
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "Timer : " + Mathf.FloorToInt(timer) + "sec";
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score : " + score;
    }

    public void AddCount()
    {
        count++;
        countText.text = "Count : " + count.ToString("D2");
    }
}
