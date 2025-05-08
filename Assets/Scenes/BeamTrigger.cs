using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            Human human = other.GetComponent<Human>();
            if (human != null)
            {
                GameManager.Instance.AddScore(human.scoreValue);
                GameManager.Instance.AddCount();
                Destroy(other.gameObject);
            }
        }
    }
}

