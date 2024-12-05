using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // retrives value of the horizontal axis ranging from -1 - 1
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime); // rotates the object along the horizontal plane 
    }
}
