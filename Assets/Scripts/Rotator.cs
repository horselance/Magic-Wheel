using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 80;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0f, 0f, rotationSpeed); 
    }
}
