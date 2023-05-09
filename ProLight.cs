using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProLight : MonoBehaviour
{
    public float changeSpeed;
    void LateUpdate()
    {
        if(!GameManager.isGameOver)
        transform.Rotate(Vector2.right * Time.deltaTime * changeSpeed);
    }
}
