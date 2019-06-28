using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    Light lightComponent;

    float lightIntensity; //明るさの強度
    bool lightOnFlag;  //ライトが点いていればtrue

    void Start()
    {
        lightComponent = GetComponent<Light>();
        lightIntensity = lightComponent.intensity;
        lightOnFlag = true;
    }
    

    void Update()
    {
        //transform.localRotation = GameObject.Find("Camera").transform.localRotation;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
        
    }
    

    public void LightSwitching()
    {
        if (lightOnFlag == false)
        {
            lightComponent.intensity = lightIntensity;
            lightOnFlag = true;
        }

        else
        {
            lightComponent.intensity = 0;
            lightOnFlag = false;
        }
    }
}
