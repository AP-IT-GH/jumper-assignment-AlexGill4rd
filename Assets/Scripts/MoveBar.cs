using System;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to control the speed of the bar
    public float endPositionZ = -10f;

    private void Update()
    {
        this.transform.Translate(Vector3.back * Math.Abs(speed) * Time.deltaTime);
    }
}
