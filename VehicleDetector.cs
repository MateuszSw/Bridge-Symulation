using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VehicleDetector : MonoBehaviour
{
    public string vehicleTag;
    public int CrossingVehicles = 0;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == vehicleTag)
            CrossingVehicles += 1;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == vehicleTag)
            CrossingVehicles -= 1;
    }

    public bool Detected
    {
        get
        {
            return CrossingVehicles > 0;
        }
    }
}
