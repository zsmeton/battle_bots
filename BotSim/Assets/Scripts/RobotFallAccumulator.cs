using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFallAccumulator : MonoBehaviour
{
    private RobotFallSimulation[] robotFallSimulations;
    public float SuccessRate = 0f;
    public float UprightRate = 0f;
    public float TwoWheelRate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        robotFallSimulations = transform.GetComponentsInChildren<RobotFallSimulation>();
    }

    // Update is called once per frame
    void Update()
    {
        int success = 0;
        int upright = 0;
        int total = 0;
        int twoWheels = 0;
        foreach (RobotFallSimulation simulation in robotFallSimulations) {
            success += simulation.WheelFalls;
            total += simulation.TotalFalls;
            upright += simulation.UprightFalls;
            twoWheels += simulation.TwoWheelFalls;
        }

        SuccessRate = (float)success / (float)total * 100f;
        UprightRate = (float)upright / (float)total * 100f;
        TwoWheelRate = (float)twoWheels / (float)total * 100f;


    }
}
