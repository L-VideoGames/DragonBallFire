using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBomb : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 growthScaleRate;
    [SerializeField] private int rotate;
    void Update()
    {
        transform.localScale += growthScaleRate * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotate) * Time.deltaTime);
    }


}
