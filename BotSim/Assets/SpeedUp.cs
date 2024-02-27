using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float Scale = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 2f;

    }

    private void FixedUpdate()
    {
        Time.timeScale = 2f;
    }
}
