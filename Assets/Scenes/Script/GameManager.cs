using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public float timer = 0f;
    public float timeLimit = 60f; // 제한 시간 1분
    private bool isGameOver = false;
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text nextTierText;



    public TMP_Text scoreText;
    public TMP_Text timerText;


    // 점수에 따라 보여줄 이미지 오브젝트들
    public GameObject result_Failure;
    public GameObject result_Good;
    public GameObject result_Great;
    public GameObject result_Amazing;

    // 결과 사운드
    public AudioSource sfxSource;          
    public AudioClip sfxFailure;
    public AudioClip sfxGood;
    public AudioClip sfxGreat;
    public AudioClip sfxAmazing;
    public float sfxVolume = 0.8f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        result_Failure.SetActive(false);
        result_Good.SetActive(false);
        result_Great.SetActive(false);
        result_Amazing.SetActive(false);
    }


    private void Update()
    {
        if (isGameOver) return; // 게임 종료되면 더 이상 처리 안 함

        timer += Time.deltaTime;

        // 시간 UI 업데이트
        int timeLeft = Mathf.FloorToInt(timeLimit - timer);
        timerText.text = $"Timer : {Mathf.Max(timeLeft, 0)}초";

        // 제한 시간 초과 시 게임 종료
        if (timer >= timeLimit)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f; // 게임 정지

        gameOverPanel.SetActive(true); // UI 표시
        finalScoreText.text = $"Score: {score}";
        ShowResultImage(score);
        ShowNextTierInfo(score);
        PlayResultSfx(score);
    }

    private void ShowResultImage(int score)
    {
        // 모든 결과 이미지 숨겨놓기
        result_Failure.SetActive(false);
        result_Good.SetActive(false);
        result_Great.SetActive(false);
        result_Amazing.SetActive(false);

        // 점수에 따라 특정 이미지 Active
        if (score >= 500)
            result_Amazing.SetActive(true);
        else if (score >= 400)
            result_Great.SetActive(true);
        else if (score >= 300)
            result_Good.SetActive(true);
        else
            result_Failure.SetActive(true);
    }


    public void AddScore(int value)
    {
        score += value;
        scoreText.text = $"Score : {score}점";

    }

    // 작동안함. 디버그 출력만 함
    public void AddCount()
    {
        Debug.Log("AddCount called - but no longer used.");
    }

    private void ShowNextTierInfo(int score)
    {
        string message = "";

        if (score < 300)
        {
            int remain = 300 - score;
            message = $"GOOD까지 {remain}점 남았어요!";
        }
        else if (score < 400)
        {
            int remain = 400 - score;
            message = $"GREAT까지 {remain}점 남았어요!";
        }
        else if (score < 500)
        {
            int remain = 500 - score;
            message = $"AMAZING까지 {remain}점 남았어요!";
        }
        else
        {
            message = "최고 등급에 도달했어요!";
        }

        nextTierText.text = message;
    }
    //결과에 따른 사운드 재생
    private void PlayResultSfx(int scoreValue)
    {
        AudioClip clip = null;

        if (scoreValue >= 500) clip = sfxAmazing;
        else if (scoreValue >= 400) clip = sfxGreat;
        else if (scoreValue >= 300) clip = sfxGood;
        else clip = sfxFailure;

        if (clip == null) return;

        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;   // 볼륨 통일
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            // 2D로 안전 재생(임시 오브젝트)
            AudioSource.PlayClipAtPoint(clip, Camera.main ? Camera.main.transform.position : Vector3.zero, sfxVolume);
        }
    }
}
