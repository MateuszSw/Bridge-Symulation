using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingDetector : MonoBehaviour
{
    public string vehicleTag;
    public int Passed = 0;

    void OnTriggerExit(Collider other)
    {
        if (other.tag == vehicleTag)
            Passed += 1;
    }
}
