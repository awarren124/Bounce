using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	// Use this for initialization

    public Button[] buttons;
    public Sprite[] skins;
    public GameObject platform;
    int numOfItems = 8;
    bool[] unlocked;
    Color lockedColor = Color.gray;
    Color unlockedColor = Color.blue;
    Color activeColor= Color.yellow;

	void Start () {
        if(!PlayerPrefs.HasKey("unlocked")){
            for(int i = 0; i < numOfItems; i++){
                unlocked = new bool[numOfItems];
                unlocked[i] = false;
            }
            PlayerPrefsX.SetBoolArray("unlocked", unlocked);
            PlayerPrefs.Save();
        }else{
            unlocked = PlayerPrefsX.GetBoolArray("unlocked");
        }

        for(int i = 0; i < numOfItems; i++){
            if(unlocked[i]){
                buttons[i].GetComponent<Image>().color = unlockedColor;
                setShopPicture(i);
            }else{
                buttons[i].GetComponent<Image>().color = lockedColor;
            }
        }
        buttons[PlayerPrefs.GetInt("ActiveSkin")].GetComponent<Image>().color = activeColor;
	}
	
    public void shopIn(){
        GetComponent<Animation>()["ShopIn"].speed = 1;;
        GetComponent<Animation>()["ShopIn"].time = 0.0F;
        GetComponent<Animation>().Play("ShopIn");
    }

    public void buttonPressed(int index){
        if(check() && !unlocked[index]){
            unlock(index);
        }

        if(unlocked[index]){
            setActiveSkin(index);
        }
    }

    bool check(){
        if(PlayerPrefs.GetInt("Stars") >= 1){
            return true;
        }
        return false;
    }

    void unlock(int index){
        unlocked[index] = true;
        PlayerPrefsX.SetBoolArray("unlocked", unlocked);
        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") - 1);
        setActiveSkin(index);
        PlayerPrefs.Save();
        buttons[index].GetComponent<Image>().color = unlockedColor;
        setShopPicture(index);
    }

    void setActiveSkin(int index){
        int oldActive = PlayerPrefs.GetInt("ActiveSkin");
        buttons[oldActive].GetComponent<Image>().color = unlockedColor;
        buttons[index].GetComponent<Image>().color = activeColor;
        PlayerPrefs.SetInt("ActiveSkin", index);
        PlayerPrefs.Save();
        platform.GetComponent<SpriteRenderer>().sprite = skins[index];
    }

    public void shopOut(){
        PlayerPrefs.Save();
        GetComponent<Animation>()["ShopIn"].speed = -1;
        GetComponent<Animation>()["ShopIn"].time = GetComponent<Animation>()["ShopIn"].length;
        GetComponent<Animation>().Play("ShopIn");
        GameManager.ui.menuIn(false, false);
    }

    void setShopPicture(int index){
        foreach(Transform child in buttons[index].transform) {
            if(child.CompareTag("SkinPreview")) {
                child.gameObject.GetComponent<Image>().sprite = skins[index];
            }
        }
    }
}
