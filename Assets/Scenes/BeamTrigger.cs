using UnityEngine;
using System.Collections;

public class BeamTrigger : MonoBehaviour
{
    public float captureTimeRequired = 2f; // 2초 동안 포획
    public CaptureUI captureUI;


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Triggered with: " + other.name); 
        if (other.CompareTag("Human"))
        {
            Debug.Log("Human 감지됨!");

            Human human = other.GetComponent<Human>();
            if (human != null && !human.isCaptured)
            {
                if (Input.GetKey(KeyCode.Space))
                {   
                    Debug.Log("스페이스바 눌림");
                    human.captureTimer += Time.deltaTime;

                    float progress = Mathf.Clamp01(human.captureTimer / captureTimeRequired);
                    // 바 위치를 현재 타겟 인간 위로 옮김
                    captureUI.transform.position = human.transform.position + Vector3.up * 2f;

                    captureUI.SetProgress(progress);
                    captureUI.Show(true);


                    if (human.captureTimer >= captureTimeRequired)
                    {
                        human.isCaptured = true;
                        captureUI.Show(false);
                        StartCoroutine(Abduct(human));
                    }
                }
                else
                {
                    human.captureTimer = 0f;
                    captureUI.Show(false);
                }
            }
        }
    }

    private IEnumerator Abduct(Human human)
    {
        Vector3 start = human.transform.position;
        Vector3 target = start + Vector3.up * 2f;
        float duration = 1.5f;
        float elapsed = 0f;

        // 포획 애니메이션
        while (elapsed < duration)
        {
            if (human == null) yield break;
            human.transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        GameManager.Instance.AddScore(human.scoreValue);
        GameManager.Instance.AddCount();

        Destroy(human.gameObject);
    }
}
