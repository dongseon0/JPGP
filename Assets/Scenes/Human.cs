using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public int scoreValue = 10;

    public bool isInBeam = false;
    public float captureTimer = 0f;

    public bool isCaptured = false; // 이미 잡혔는지 여부
}
