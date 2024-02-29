using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshPro))]
public class DisplaySimulationData : MonoBehaviour
{
    public RobotFallAccumulator simulation;
    private TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro> ();
    }

    // Update is called once per frame
    void Update()
    {
        string format = "{0, -10}| {1,5} | {2, 4} |\n";
        text.SetText("<mspace=1em>" + string.Format(format, "", "%  ", "E(f)") +
                    string.Format(format, "One Wheel", simulation.SuccessRate.ToString("G4"), ExpectedFailure(simulation.SuccessRate)) +
                    string.Format(format, "Two Wheels", simulation.TwoWheelRate.ToString("G4"), ExpectedFailure(simulation.TwoWheelRate)) +
                    string.Format(format, "Upright", simulation.UprightRate.ToString("G4"), ExpectedFailure(simulation.UprightRate)));
    }

    int ExpectedFailure(float percentage)
    {
        if (float.IsNaN(percentage) || percentage >= 99.99)
            return -1;
        
        float inversePercentage = 1 - percentage / 100f;
        int expected = Mathf.RoundToInt(1f / inversePercentage);
        return expected;
    }
}
