using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{

    public CanvasGroup catalog;
    public GameObject exitButton;
    public GameObject catalogButton;
    public GameObject playerUI; //i want to be able to hide player ui while the catalog is up

    private void Start()
    {
        catalog = GameObject.FindGameObjectWithTag("Catalog").GetComponent<CanvasGroup>();    
        exitButton = GameObject.FindGameObjectWithTag("ExitButton");
        catalogButton = GameObject.FindGameObjectWithTag("CatalogButton");
        playerUI = GameObject.FindGameObjectWithTag("Player");
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
        playerUI.SetActive(true);
    }

    public void onCatalogMenuButtonClick()
    {
        catalogButton.SetActive(false);
        catalog.alpha = 1f;
        catalog.blocksRaycasts = true;
        catalog.interactable = true;
        playerUI.SetActive(false);
    }
}
