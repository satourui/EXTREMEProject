using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlashLight : MonoBehaviour
{
    Light light;
    float lightIntensity;


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        lightIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
