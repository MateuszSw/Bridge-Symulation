using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBehaviour : MonoBehaviour
{
    public Text textCarsCount;
    public Text textShipsCount;
    public Text textSimulationSpeed;

    public Slider sliderCarsCount;
    public Slider sliderShipsCount;
    public Slider sliderSimulationSpeed;

    public TimeScaler timeScaler;
    public SimulationController simulationController;


    void Update ()
    {
        int carsCount = (int)sliderCarsCount.value;
        int shipsCount = (int)sliderShipsCount.value;
        float simulationSpeed = sliderSimulationSpeed.value;

        textCarsCount.text = carsCount.ToString();
        textShipsCount.text = shipsCount.ToString();
        textSimulationSpeed.text = simulationSpeed.ToString();


        timeScaler.timeScale = simulationSpeed;
        simulationController.maxStoppedCars = carsCount;
        simulationController.maxStoppedShips= shipsCount;
    }
}
