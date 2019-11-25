using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlashLight : MonoBehaviour
{

    private Transform cameraObj;

    private FlashLightController flash;

    //private Ray ray;

    public GameObject lightTarget;

    Light lightObj;

    private float lightIntensity;

    private bool inBlock;

    private float distance;
    private float spotMax = 100f;

    private float lightSpot;
    private float lightSpotAngle;

    // Start is called before the first frame update
    void Start()
    {
        //cameraObj = GameObject.Find("Camera(Clone)").GetComponent<Camera>();
        //cameraController = cameraObj.GetComponent<CameraController>();

        //ray = new Ray(transform.position, lightTarget.transform.position);

        flash = GetComponent<FlashLightController>();

        lightObj = GetComponent<Light>();
        lightIntensity = lightObj.intensity;

        inBlock = false;

        distance = 0;

        lightSpotAngle = lightObj.spotAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraObj == null)
        {
            cameraObj = GameObject.FindGameObjectWithTag("BigCamera").GetComponent<Transform>();
        }

        transform.rotation = cameraObj.rotation;

        #region Rayできなかったやつ
        //Ray

        //Debug.DrawLine(transform.position, lightTarget.transform.position , Color.red);
        //Debug.DrawLine(transform.position, lightTarget.transform.position, Color.red);

        //float rayDistance = (transform.position - lightTarget.transform.position).magnitude;

        ////Ray ray = Camera.main.ScreenPointToRay(new Vector3(200, 200, 0));

        ////Rayが当たったオブジェクトの情報を入れる箱
        //RaycastHit hit;
        //if (flash.LightOnFlag)
        //{
        //    if (Physics.Raycast(ray, out hit, 10))
        //    {
        //        //Debug.Log(hit.collider.gameObject);


        //        float distance = (transform.position - hit.collider.gameObject.transform.position).magnitude;

        //        if (distance < 10)
        //        {

        //            float f = Mathf.Abs(lightIntensity - distance);

        //            Debug.Log(f);

        //            if (f < 1)
        //            {
        //                f = 1;
        //            }

        //            float dis = distance;
        //            if(dis > lightIntensity)
        //            {
        //                dis = lightIntensity;
        //            }
        //            if (dis < 1)
        //            {
        //                dis = 1;
        //            }

        //            lightObj.intensity = dis;

        //            //lightObj.intensity = lightObj.intensity / f;
        //            Debug.Log("入った");
        //        }
        //        else
        //        {
        //            lightObj.intensity = lightIntensity;
        //        }

        //    }
        //}

        #endregion

        //if (flash.LightOnFlag)
        //{
        //    if (inBlock)
        //    {
        //        float dis = distance;
        //        if (dis > lightIntensity)
        //        {
        //            dis = lightIntensity;
        //        }
        //        if (dis < 0.5f)
        //        {
        //            dis = 0.5f;
        //        }
        //        lightObj.intensity = dis;


        //        float spot = spotMax - (distance * 6f);
        //        if (spot < lightSpotAngle)
        //        {


        //            spot = lightSpotAngle;
        //        }
        //        if (spot > spotMax)
        //        {
        //            spot = spotMax;
        //        }

        //        lightObj.spotAngle = spot;
        //    }
        //    else if (!inBlock)
        //    {
        //        lightObj.intensity += 0.05f;

        //        if (lightObj.intensity > lightIntensity)
        //        {
        //            lightObj.intensity = lightIntensity;
        //        }



        //        if (lightObj.spotAngle < lightSpotAngle)
        //        {
        //            lightObj.spotAngle = spotMax - (distance * 7f);
        //        }

        //        if (lightObj.spotAngle < lightSpotAngle)
        //        {
        //            lightObj.spotAngle = lightSpotAngle;
        //        }
        //    }
        //}
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Block" && flash.LightOnFlag)
        {
            inBlock = true;

            Vector3 hitPos = col.ClosestPointOnBounds(this.transform.position);

            //distance = (transform.position - col.gameObject.transform.position).magnitude;
            distance = (transform.position - hitPos).magnitude;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Block")
        {
            inBlock = false;
        }
    }

}
