using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMover : MonoBehaviour
{
    [SerializeField] Vector3 moveTo;

    void Update()
    {
        transform.position += moveTo * Time.deltaTime;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        this.moveTo = newVelocity;
    }
}
