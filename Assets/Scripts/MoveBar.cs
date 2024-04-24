using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBar : MonoBehaviour
{
    public float speed = 5f; // Adjust this value to control the speed of the bar
    public float endPositionZ = -10f;
 
    private void Update()
    {
        this.transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
