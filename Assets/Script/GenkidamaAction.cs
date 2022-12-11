using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenkidamaAction : MonoBehaviour
{
    [SerializeField] private Vector3 moveTo;
    [SerializeField] private int rotate;
    void Update()
    {
        transform.position += moveTo * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotate) * Time.deltaTime);
    }

    public void SetDirection(int direction)
    {
        
        this.moveTo = direction * this.moveTo;
        
    }
}
