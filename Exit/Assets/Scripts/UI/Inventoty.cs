using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventoty : MonoBehaviour
{
    Image image;
    Sprite sprite;
    PlayerController player;
    List<GameObject> itemList;

    void Start()
    {
        image = GameObject.Find("CurrentItem").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        itemList = player.ItemList;
    }

    // Update is called once per frame
    void Update()
    {
        if(/*player.State==PlayerController.PlayerState.Normal*/
            GamePlayManager.instance.State==GamePlayManager.GameState.Play)
        {
            //アイテムを持っていなければ
            if (player.ItemQuantity == 0)
            {
                image.gameObject.SetActive(false);
                return;
            }

            image.gameObject.SetActive(true);
            Texture2D texture = itemList[player.ItemNum].GetComponent<ItemObj>().ItemIcon;
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            image.sprite = sprite;
        }
        
    }
}
