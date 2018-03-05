using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleDeleter : MonoBehaviour
{
    public string vehicleTag;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == vehicleTag)
        {
            GameObject objectToDelete = other.transform.parent.gameObject;
            Destroy(objectToDelete);
        }
    }
}
