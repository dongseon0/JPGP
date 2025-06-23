using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioClip clip; 
    private AudioSource audioSource;

    private void Awake()
    {
        // AudioSource 자동 추가 (중복 방지)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    // 버튼에서 이 함수 호출하면 소리 재생됨
    public void PlayClick()
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("UISound: AudioClip이 비어 있습니다.");
        }
    }
}
