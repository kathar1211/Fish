using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catalog : MonoBehaviour
{

    public List<string> fishCaughtList;
    public List<GameObject> panelList;
    public GameObject fishPanel;

    public Sprite silhouette;

    // Start is called before the first frame update
    void Start()
    {
       
        InitializePanel();
    }

    /// <summary>
    /// runs on start, intializes catalog with silhouettes
    /// </summary>
    public void InitializePanel()
    {
        for (int i = 0; i < 23; i++)
        {
            Debug.Log(i);
            panelList[i] = GameObject.Find("FishPos1 ("+i+")");
            panelList[i].GetComponent<Image>().sprite = silhouette;

        }
    }

    /// <summary>
    /// runs every time a fish is caught
    /// </summary>
    /// <param name="fishCaught"></param>
    public void FishCaught(Fish fishCaught)
    {

        //insert animation where fish gets added to catalog?
        

        //if the fish caught is not in the list of fish that have already been caught
        if (fishCaughtList.Contains(fishCaught._name)==false)
        {
            fishCaughtList.Add(fishCaught._name);
            //change sprite of corresponding panel to the fish's sprite rather than silhouette
            switch (fishCaught.name)
            {
                case "Salmon":
                    panelList[11].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Tuna":
                    panelList[20].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Pufferfish":
                    panelList[10].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Eel":
                    panelList[5].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Catfish":
                    panelList[1].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Squid":
                    panelList[16].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Octopus":
                    panelList[8].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Baby Shark":
                    panelList[0].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Vegan 'Sushi'":
                    panelList[9].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Crab":
                    panelList[3].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Flounder":
                    panelList[6].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Sardine":
                    panelList[12].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Starfish":
                    panelList[17].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Stingray":
                    panelList[18].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Sea Cucumber":
                    panelList[13].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Clownfish":
                    panelList[2].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Yellow Tang":
                    panelList[21].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Dory?":
                    panelList[4].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Trumpeter":
                    panelList[19].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Sea Urchin":
                    panelList[15].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Jellyfish":
                    panelList[7].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
                case "Sea Turtle":
                    panelList[14].GetComponent<Image>().sprite = fishCaught._fishSprite;
                    break;
            }

            if (fishCaughtList.Count==22)
            {
                GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>().onGameEnd();
            }

        }

        

    }
    /// <summary>
    /// passing through a string instead of fish, made this for testing but i can expand it if we need it
    /// </summary>
    /// <param name="fishCaught"></param>
    public void FishCaught(string fishCaught)
    {
        if (fishCaughtList.Contains(fishCaught) == false)
        {
            fishCaughtList.Add(fishCaught);
            switch (fishCaught)
            {
                case "Salmon":
                    panelList[11].GetComponent<Image>().sprite = null;
                    break;
                case "Tuna":
                    panelList[20].GetComponent<Image>().sprite = null;
                    break;
                case "Pufferfish":
                    panelList[10].GetComponent<Image>().sprite = null;
                    break;
                case "Eel":
                    panelList[5].GetComponent<Image>().sprite = null;
                    break;
                case "Catfish":
                    panelList[1].GetComponent<Image>().sprite = null;
                    break;
            }
        }
    }

}
