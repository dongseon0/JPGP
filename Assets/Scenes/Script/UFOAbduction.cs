using UnityEngine;

public class UFOAbduction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            Human human = other.GetComponent<Human>();
            if (human != null)
            {
                GameManager.Instance.AddScore(human.scoreValue);
            }

            Destroy(other.gameObject); // 납치된 인간 제거
        }
    }
}
