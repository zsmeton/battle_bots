using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAway(Camera.transform);
    }

    void LookAway(Transform target)
    {
        Vector3 lookDirection = transform.position - target.position;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
