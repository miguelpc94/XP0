using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandomRotation : MonoBehaviour
{
    [SerializeField] float maxRandomTorque = 5000;
    void Start()
    {
        transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddTorque(new Vector3(Random.Range(0, maxRandomTorque), Random.Range(0, maxRandomTorque), Random.Range(0, maxRandomTorque)));
    }

}
