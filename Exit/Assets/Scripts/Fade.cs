using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //フェード処理に必要な値達
    float alpha;//透明値を操作するための値
    float red, green, blue;//RGBを操作するための値
    public float speed = 0.1f; //フェードの速度

    // Start is called before the first frame update
    void Start()
    {
        //暗転パネルの各色を取得
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = new Color(red, green, blue, alpha);
        alpha += speed;
    }
}
