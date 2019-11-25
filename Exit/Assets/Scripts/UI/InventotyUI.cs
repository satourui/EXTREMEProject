using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventotyUI : MonoBehaviour
{
    public GameObject inventoryObj;
    public GameObject currentItemObj;

    Image currentItemImage;
    Sprite sprite;
    //PlayerController pc;
    [SerializeField]
    private List<GameObject> itemList;

    public List<GameObject> ItemList { get => itemList; set => itemList = value; }

    void Start()
    {
        currentItemImage = currentItemObj.GetComponent<Image>();
        //pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //pc = GamePlayManager.instance.Player.GetComponent<PlayerController>();
        //itemList = pc.ItemList;
    }

    // Update is called once per frame
    void Update()
    {
        var gameState = GamePlayManager.instance.State;
        

        if (gameState == GamePlayManager.GameState.Play ||
            gameState == GamePlayManager.GameState.Talk)
        {
            if (!inventoryObj.activeSelf)
            {
                inventoryObj.SetActive(true);
            }

            var pc = GamePlayManager.instance.PC;
            
            //アイテムを持っていなければ
            if (pc.ItemList.Count==0)
            {
                currentItemImage.gameObject.SetActive(false);
                return;
            }
            

            currentItemImage.gameObject.SetActive(true);
            Texture2D texture = pc.ItemList[pc.ItemNum].GetComponent<ItemObj>().ItemIcon;
            sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            currentItemImage.sprite = sprite;
        }

        else
        {
            if (inventoryObj.activeSelf)
            {
                inventoryObj.SetActive(false);
            }
        }
        
    }
}
