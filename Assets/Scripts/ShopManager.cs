using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	// Use this for initialization

    public Button[] buttons;
    public Sprite[] skins;
    public GameObject platform;
    int numOfItems = 7;
    bool[] unlocked;
    public Color lockedColor;
    public Color unlockedColor;
    public Color activeColor;
    int price = 50;

	void Start () {

        //PlayerPrefs initialization
        if(!PlayerPrefs.HasKey("ActiveSkin")) {
            PlayerPrefs.SetInt("ActiveSkin", 0);
        }
        if(!PlayerPrefs.HasKey("unlocked")){
            unlocked = new bool[numOfItems];
            for(int i = 0; i < numOfItems; i++){
                unlocked[i] = false;
            }
            unlocked[0] = true;
            PlayerPrefsX.SetBoolArray("unlocked", unlocked);
            PlayerPrefs.Save();
        }else{
            unlocked = PlayerPrefsX.GetBoolArray("unlocked");
        }

        //Loops through shop items and formats according to whether they are unlocked or not
        for(int i = 0; i < numOfItems; i++){
            if(unlocked[i]){
                buttons[i].GetComponent<Image>().color = unlockedColor;
                hidePrice(i);
                setShopPicture(i);
            }else{
                buttons[i].GetComponent<Image>().color = lockedColor;
            }
        }
        buttons[PlayerPrefs.GetInt("ActiveSkin")].GetComponent<Image>().color = activeColor;
        setActiveSkin(PlayerPrefs.GetInt("ActiveSkin"));
	}
	
    //Plays animation for the shop entering
    public void shopIn(){
        GetComponent<Animation>()["ShopIn"].speed = 1;
        GetComponent<Animation>()["ShopIn"].time = 0.0F;
        GetComponent<Animation>().Play("ShopIn");
    }

    //Called when an item in the shop is pressed
    public void buttonPressed(int index){

        //If the item is locked, it checks it then unlocks if there is enough stars
        if(check() && !unlocked[index]){
            unlock(index);
        }

        //If the item is unlocked it is set active
        if(unlocked[index]){
            setActiveSkin(index);
        }
    }

    //Checks whether the user has enough stars
    bool check(){
        if(PlayerPrefs.GetInt("Stars") >= price){
            return true;
        }
        return false;
    }

    //Unlocks item
    void unlock(int index){
        unlocked[index] = true;
        PlayerPrefsX.SetBoolArray("unlocked", unlocked);
        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - price);
        hidePrice(index);
        setActiveSkin(index);
        PlayerPrefs.Save();
        buttons[index].GetComponent<Image>().color = unlockedColor;
        setShopPicture(index);
    }

    //Sets skin to active
    void setActiveSkin(int index){
        int oldActive = PlayerPrefs.GetInt("ActiveSkin");
        buttons[oldActive].GetComponent<Image>().color = unlockedColor;
        buttons[index].GetComponent<Image>().color = activeColor;
        PlayerPrefs.SetInt("ActiveSkin", index);
        PlayerPrefs.Save();
        platform.GetComponent<SpriteRenderer>().sprite = skins[index];
        platform.GetComponent<Platform>().setDrawMode(index);
        foreach(Transform child in buttons[index].transform) {
            if(child.CompareTag("PriceText")) {
                child.gameObject.GetComponent<Text>().text = "Selected";
            }
        }
        if(oldActive != index) {
            foreach(Transform child in buttons[oldActive].transform) {
                if(child.CompareTag("PriceText")) {
                    child.gameObject.GetComponent<Text>().text = "Unlocked";
                }
            }
        }

    }

    //Plays animation for the shop exiting
    public void shopOut(){
        PlayerPrefs.Save();
        GetComponent<Animation>()["ShopIn"].speed = -1;
        GetComponent<Animation>()["ShopIn"].time = GetComponent<Animation>()["ShopIn"].length;
        GetComponent<Animation>().Play("ShopIn");
        GameManager.ui.menuIn(false, false);
        GameManager.ui.hideStarsLabel();
    }

    //Sets the picture for an unlocked shop item
    void setShopPicture(int index){
        foreach(Transform child in buttons[index].transform) {
            if(child.CompareTag("SkinPreview")) {
                child.gameObject.GetComponent<Image>().sprite = skins[index];
                child.gameObject.GetComponent<Image>().transform.localScale = Vector3.one;

                switch(index) {
                    case 0:
                        child.gameObject.GetComponent<Image>().type = Image.Type.Simple;
                        break;
                    case 1:
                    case 3:
                    case 5:
                    case 6:
                        child.gameObject.GetComponent<Image>().type = Image.Type.Sliced;
                        break;
                    case 2:
                    case 4:
                        child.gameObject.GetComponent<Image>().type = Image.Type.Tiled;
                        break;
                    default:
                        child.gameObject.GetComponent<Image>().type = Image.Type.Simple;
                        break;
                }

                
                child.gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(185, 100);
            }
        }
    }

    //Hides the price of shop item
    void hidePrice(int index){
        foreach(Transform child in buttons[index].transform) {
            if(child.CompareTag("PriceText")) {
                child.gameObject.GetComponent<Text>().text = "Unlocked";
            } else if(child.CompareTag("PriceStar")) {
                Color temp = child.gameObject.GetComponent<Image>().color;
                temp.a = 0F;
                child.gameObject.GetComponent<Image>().color = temp;
            }
        }
    }
}
