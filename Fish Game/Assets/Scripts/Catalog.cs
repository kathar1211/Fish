using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Catalog : MonoBehaviour
{

    public List<Fish> fishList;
    public List<GameObject> panelList;
    public GameObject fishPanel;
    int index;

    public Sprite silhouette;

    // Start is called before the first frame update
    void Start()
    {
       
        InitializePanel();
        InitializeList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePanel()
    {
        index = 0;
        for (int i = 0; i < fishList.Count+1; i++)
        {
            Debug.Log(index);
            panelList[i] = GameObject.Find("FishPos1 ("+index+")");
            index++;

            panelList[i].GetComponent<Image>().sprite = silhouette;

        }
    }

    public void InitializeList()
    {
        foreach (var item in fishList)
        {

            if (item.isCaught)
            {
                panelList[item.posInList].GetComponent<Image>().sprite = item._fishSprite;
            }
        }
    }
}
