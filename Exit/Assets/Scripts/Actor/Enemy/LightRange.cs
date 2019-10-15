using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRange : MonoBehaviour
{
    private Light light;

    private float L_range;
    private float angle;

    private Vector3 position;
    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        position = transform.position;

        L_range = light.range;
        angle = light.spotAngle;

        var a = angle / 2;

        var tan = Mathf.Tan(a * (Mathf.PI / 180.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
