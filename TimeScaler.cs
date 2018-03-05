using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{

    [Range(0.1f, 50)]
    public float timeScale = 1;

    void Update()
    {
        Time.timeScale = timeScale;
    }



}
