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
        lightIntensity = lightComponent.intensity;  //インベントリで設定されている明るさを保存
        lightOnFlag = false;  //最初は点いていない状態
    }
    

    void Update()
    {
        //ライトが点いている状態なら
        if (lightOnFlag)
        {
            lightComponent.intensity = lightIntensity;
        }

        //ライトが点いていないなら
        else
        {
            lightComponent.intensity = 0;
        }
    }
    

    public void LightSwitching()
    {
        if (lightOnFlag == false)
        {
            lightOnFlag = true;
        }

        else
        {
            lightOnFlag = false;
        }
        
    }
}
