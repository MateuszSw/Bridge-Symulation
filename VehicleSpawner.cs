using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleSpawner : MonoBehaviour
{

    public Vehicle vehicle;

    public Transform spawnPoint;
    public VehicleDetector vehicleDetector;

    public int stopped = 0;

    public float spawnMinTime = 1;

    public float spawnMaxTime = 2;

    private float timeToNextSpawn = 0;

    void Start()
    {
        timeToNextSpawn = Random.Range(spawnMinTime, spawnMaxTime);
    }

    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;

        if (vehicleDetector.Detected)
            return;
        

        if (timeToNextSpawn <= 0)
        {

            Vehicle instantiated = Instantiate(vehicle, spawnPoint.position, spawnPoint.rotation, transform) as Vehicle;
            instantiated.spawner = this;
            timeToNextSpawn = Random.Range(spawnMinTime, spawnMaxTime);
        }
    }



}
