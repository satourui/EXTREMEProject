using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    Light lightComponent;

    float lightIntensity; //明るさの強度
    bool lightOnFlag;  //ライトが点いていればtrue

    public bool LightOnFlag { get => lightOnFlag; set => lightOnFlag = value; }

    void Start()
    {
        lightComponent = GetComponent<Light>();
        lightIntensity = lightComponent.intensity;  //インベントリで設定されている明るさを保存
        LightOnFlag = false;  //最初は点いていない状態
    }
    

    void Update()
    {
        //ライトが点いている状態なら
        if (LightOnFlag)
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
        if (LightOnFlag == false)
        {
            LightOnFlag = true;
        }

        else
        {
            LightOnFlag = false;
        }

        //十字キー
        //float switchingNum = Input.GetAxisRaw("LightSwitch");
        //if (switchingNum > 0)
        //    lightOnFlag = true;

        //else if (switchingNum < 0)
        //    lightOnFlag = false;

    }

    public void SwitchOn()
    {
        LightOnFlag = true;
    }

    public void SwitchOff()
    {
        LightOnFlag = false;
    }
}
