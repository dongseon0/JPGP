using UnityEngine;

public enum HumanType { Black, Yellow, Dog }

public class Human : MonoBehaviour
{
    public HumanType type = HumanType.Black;   // Inspector에서 종류 선택
    public int scoreValue = 10;                // 포획 시 점수
    public float moveSpeed = 3f;               // AI 이동 속도

    [HideInInspector] public float captureTimer = 0f;
    [HideInInspector] public bool isCaptured = false;

    // 포획 완료 시 소리
    public AudioClip captureCompleteClip;
    private AudioSource audioSource;
    void Start()
    {
        isCaptured = false;
        captureTimer = 0f;
        audioSource = GetComponent<AudioSource>(); // 포획 시 오디오

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
                scoreValue = -10;
                break;
        }
    }
    // 포획 완료 시 호출
    public void OnCaptured()
    {
        if (captureCompleteClip == null)
        {
            Debug.LogWarning("[Human] captureCompleteClip not set on " + name);
            Destroy(gameObject);
            return;
        }

        // 분리된 임시 오브젝트에서 2D로 재생 → 위치/거리 상관없이
        PlaySfxDetached(captureCompleteClip, transform.position, 0.5f, twoD: true);

        Destroy(gameObject);
    }

    // 재생 유틸
    static void PlaySfxDetached(AudioClip clip, Vector3 worldPos, float volume = 1f, bool twoD = true, float maxDist = 50f)
    {
        var go = new GameObject("SFX_OneShot");
        go.transform.position = worldPos;

        var src = go.AddComponent<AudioSource>();
        src.clip = clip;
        src.volume = Mathf.Clamp01(volume);
        src.playOnAwake = false;

        if (twoD)
        {
            src.spatialBlend = 0f;
        }
        else
        {
            src.spatialBlend = 1f; 
            src.rolloffMode = AudioRolloffMode.Linear;
            src.minDistance = maxDist * 0.25f;
            src.maxDistance = maxDist;
            src.dopplerLevel = 0f;
        }

        src.Play();
        GameObject.Destroy(go, clip.length + 0.1f);
    }
}
