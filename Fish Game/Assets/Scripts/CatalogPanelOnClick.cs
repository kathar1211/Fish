using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogPanelOnClick : MonoBehaviour
{

    public Scrollbar scrollbar;
    public GameObject panel;
    public GameObject content;
    public Button exitCatalogButton;
    public Text nameText;
    public Image fishSprite;
    public Text description;

    private void Start()
    {
        scrollbar = GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        panel = GameObject.Find("PopupPanel");
        content = GameObject.Find("Content");
        exitCatalogButton = GameObject.Find("ExitButton").GetComponent<Button>();
    }


    public void panelOnClick()
    {

        scrollbar.interactable = false;
        panel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        panel.gameObject.GetComponent<CanvasGroup>().interactable = true;
        panel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        content.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        content.gameObject.GetComponent<CanvasGroup>().interactable = false;
        content.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        exitCatalogButton.interactable = false;


    }

    public void onExitButtonClick()
    {
        scrollbar.interactable = true;
        panel.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        panel.gameObject.GetComponent<CanvasGroup>().interactable = false;
        panel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        content.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        content.gameObject.GetComponent<CanvasGroup>().interactable = true;
        content.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

