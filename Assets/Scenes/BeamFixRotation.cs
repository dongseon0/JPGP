// BeamFixRotation.cs
using UnityEngine;

public class BeamFixRotation : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);  // 항상 수직 정렬
    }
}
