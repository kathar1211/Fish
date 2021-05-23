using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//pops up when you catch a fish
public class FishPanel : MonoBehaviour
{

    //possible backgrounds
    public Sprite star1bg;
    public Sprite star2bg;
    public Sprite star3bg;

    //where we set info
    public Image FishImage;
    public TextMeshProUGUI FishName;
    public Image BG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInfo(Fish fishdata)
    {
        FishImage.sprite = fishdata._fishSprite;
        FishName.text = fishdata._name;
        switch (fishdata._starCount)
        {
            case 1:
                BG.sprite = star1bg;
                break;
            case 2:
                BG.sprite = star2bg;
                break;
            case 3:
                BG.sprite = star3bg;
                break;
        }

    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);       
    }
}
