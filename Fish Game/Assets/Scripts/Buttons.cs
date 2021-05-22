using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    public CanvasGroup catalog;
    public GameObject exitButton;
    public GameObject catalogButton;

    private void Start()
    {
        catalog = GameObject.FindGameObjectWithTag("Catalog").GetComponent<CanvasGroup>();    
        exitButton = GameObject.FindGameObjectWithTag("ExitButton");
        catalogButton = GameObject.FindGameObjectWithTag("CatalogButton");
        catalog.alpha = 0f;
        catalog.blocksRaycasts = false;
        catalog.interactable = false;
    }

    public void onExitButtonClick()
    {
        catalog.alpha = 0f;
        catalog.blocksRaycasts = false;
        catalog.interactable = false;
        catalogButton.SetActive(true);
    }

    public void onCatalogMenuButtonClick()
    {
        //for testing, delete this after
        catalog.gameObject.GetComponent<Catalog>().FishCaught("Salmon");
        //for testing delete this after
        catalogButton.SetActive(false);
        catalog.alpha = 1f;
        catalog.blocksRaycasts = true;
        catalog.interactable = true;

    }
}
