using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//klasa modelująca zachowanie pojazdu
//odpowiada za poruszanie się do przodu, wykrywanie przeszkód i zatrzymywanie się
public class Vehicle : MonoBehaviour
{
    //obiekt który tworzy kolejne pojazdy, oraz zlicza zatrzymane pojazdy.
    public VehicleSpawner spawner;
    //czy pojazd jest zatrzymany
    public bool stopped = false;
    //prędkość, przy której pojazd zatrzymuje się (żeby nie jechał np 0.0001 km/h)
    public float tresholdSpeed = 1;

    //w km / h
    //maksymalna prędkość osiągana przez pojazd
    public float maxSpeed = 50;
    //prędkość pojazdu w danym kroku symulacji
    public float currentSpeed;

    //"maska" colliderów zatrzymujących pojazdy - aby niektóre collidery nie powodowały zatrzymania
    // (np. collider wykrywający ilość pojazdów  na moście nie powinien ich zatrzymywać)
    public LayerMask obstacleMask;
    //miejsce z którego należy "wystrzeliwać" promień wykrywający przeszkody
    public Transform rayStart;
    //długość promienia (odległość od przeszkód które wpływają na hamowanie pojazdu)
    public float rayLength = 50;
    //odległość od następnego pojazdu podczas zatrzymania
    public float gap = 5;

    //przelicznik km/h na m/s
    private float kmPerHourToMPerS = 1000f / 3600f;

    //funkcja wywoływana na początku po stworzeniu pojazdu
    void Start ()
    {
        //ustawienie prędkości na maksymalną
        currentSpeed = maxSpeed;
	}

    void Update ()
    {
        //pobranie wektora kierunku ruchu
        Vector3 forward = transform.forward;

        //zmienna z danymi "kolizji" promienia wykrywającego przeszkody
        RaycastHit hit;
        // wykrywanie czy przed pojazdem znajduje się przeszkoda
        //jeśli tak - dostosuj prędkość jazdy
        if (Physics.Raycast(rayStart.position, forward, out hit, rayLength, obstacleMask))
        {
            //obecna prędkość jazdy tym mniejsza, im bliżej przeszkody
            //wyznaczana na podstawie odległości od przeszkody i miejsca zatrzymania wynikającego z "gap"
            currentSpeed = Mathf.Lerp(0, maxSpeed, (hit.distance - gap) / rayLength);
        } else 
        //w przeciwnym wypadku przed pojazdem nie znajduje się przeszkoda
        //jeśli aktualna prędkość mniejsza niż maksymalna, przyśpieszaj
        if (currentSpeed < maxSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime);
        }

        //jeśli aktualna prędkość mniejsza od granicznej, a pojazd nie jest "zatrzymany"
        if (currentSpeed < tresholdSpeed && !stopped)
        {
            //zmień prędkość na 0
            currentSpeed = 0;
            //poinformuj klasę tworzącą pojazdy, że pojazd zatrzymał się
            spawner.stopped += 1;
            //ustaw wartość zmiennej
            stopped = true;
        }
        else 
        //else - pojazd jest zatrzymany albo prędkość jest większa od granicznej
        //if - sprawdzenie czy oba te warunki są spełnione naraz
        if (currentSpeed >= tresholdSpeed && stopped)
        {
            //zmniejszenie liczby zatrzymanych pojazdów - ten właśnie ruszył
            spawner.stopped -= 1;
            stopped = false;
        }

        //kod wykonujący rzeczywiste przesunięcie na podstawie poprzednich obliczeń
        transform.position += forward * currentSpeed * kmPerHourToMPerS * Time.deltaTime;
        
	}
}
