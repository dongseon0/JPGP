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
                    Collider humanCol = human.GetComponent<Collider>();
                    Vector3 top = humanCol.bounds.center + Vector3.up * (humanCol.bounds.extents.y + 0.3f);
                    captureUI.transform.position = top;


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
