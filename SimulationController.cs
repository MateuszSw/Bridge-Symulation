using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimulationController : MonoBehaviour
{
    public VehicleSpawner carsSpawner;
    public VehicleSpawner shipsSpawner;

    public Semaphore semaphoreForCars;
    public Semaphore semaphoreForShips;

    public VehicleDetector crossingCarsDetector;
    public VehicleDetector crossingShipsDetector;

    public PassingDetector carsPassingDetector;
    public PassingDetector shipsPassingDetector;

    public Animation bridgeAnimation;

    private bool shouldOpenCarsSemaphore = false;

    private bool shouldOpenShipsSemaphore = false;

    public int stoppedCars;
    public int stoppedShips;

    public int maxStoppedCars = 3;
    public int maxStoppedShips = 3;

    void Start()
    {
        semaphoreForCars.Open();
        semaphoreForShips.Close();

        carsPassingDetector.Passed = 0;
        shipsPassingDetector.Passed = 0;
    }

    void Update()
    {
        stoppedCars = carsSpawner.stopped;
        stoppedShips = shipsSpawner.stopped;


        if (semaphoreForCars.isOpen && carsPassingDetector.Passed >= maxStoppedCars && stoppedShips >= maxStoppedShips)
        {

            semaphoreForCars.Close();
            shouldOpenShipsSemaphore = true;
        }
        else
        if (semaphoreForShips.isOpen && shipsPassingDetector.Passed >= maxStoppedShips && stoppedCars >= maxStoppedCars)
        {

            semaphoreForShips.Close();
            shouldOpenCarsSemaphore = true;
        }

        if (shouldOpenCarsSemaphore && !crossingShipsDetector.Detected)
        {
            bridgeAnimation.Play("Close");
            shouldOpenCarsSemaphore = false;
            Invoke("OpenCarsSemaphore", 2f);
        }
        else
        if (shouldOpenShipsSemaphore && !crossingCarsDetector.Detected)
        {
            bridgeAnimation.Play("Open");
            shouldOpenShipsSemaphore = false;
            Invoke("OpenShipsSemaphore", 2f);
        }
    }

    private void OpenCarsSemaphore()
    {
        semaphoreForCars.Open();
        carsPassingDetector.Passed = 0;
    }

    private void OpenShipsSemaphore()
    {
        semaphoreForShips.Open();
        shipsPassingDetector.Passed = 0;
    }


}
