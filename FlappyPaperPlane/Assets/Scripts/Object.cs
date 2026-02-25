using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public static float speed;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
    
}
