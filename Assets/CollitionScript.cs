using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionScript : MonoBehaviour
{
    public DemoAgent script;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Contains("Obstacle"))
        {
            Debug.Log("collided");
            //script.Barrier_PlayerTouched();
        }
    }
}
