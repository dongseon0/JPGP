using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 lookDir = Camera.main.transform.position - transform.position;
        lookDir.y = 0; // 수직 축 무시
        transform.forward = -lookDir.normalized;
    }
}
