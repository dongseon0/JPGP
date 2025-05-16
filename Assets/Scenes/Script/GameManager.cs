using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int count = 0;
    public float timer = 0f;
    public float timeLimit = 90f; // 제한 시간 1분 30초
     private bool isGameOver = false;
     public GameObject gameOverPanel;
    public TMP_Text finalScoreText;



    public TMP_Text scoreText;
    public TMP_Text countText;
    public TMP_Text timerText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    private void Update()
    {
        if (isGameOver) return; // 게임 종료되면 더 이상 처리 안 함

        timer += Time.deltaTime;

        // 시간 UI 업데이트
        int timeLeft = Mathf.FloorToInt(timeLimit - timer);
        timerText.text = "Timer : " + Mathf.Max(timeLeft, 0) + "sec";

        // 제한 시간 초과 시 게임 종료
        if (timer >= timeLimit)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f; // 게임 정지

        gameOverPanel.SetActive(true); // UI 표시
        finalScoreText.text = $"Game Over\nScore: {score}\nCount: {count:D2}";
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
