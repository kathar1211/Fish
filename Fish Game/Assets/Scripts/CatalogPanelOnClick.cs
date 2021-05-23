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
    public Button exitPanelButton;
    public Text nameText;
    public Image fishSprite;
    public Text description;
    public Sprite star1bg;
    public Sprite star2bg;
    public Sprite star3bg;
    public Sprite silhouette;

    private void Start()
    {
        scrollbar = GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        panel = GameObject.Find("PopupPanel");
        content = GameObject.Find("Content");
        exitCatalogButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitPanelButton = GameObject.Find("PopupExitButton").GetComponent<Button>();

        nameText = GameObject.Find("NameText").GetComponent<Text>();
        description = GameObject.Find("DescText").GetComponent<Text>();
        fishSprite = GameObject.Find("FImage").GetComponent<Image>();

        nameText.text = "You have not discovered this fish yet!";
        description.text = "You have not discovered this fish yet!";
        fishSprite.sprite = silhouette;

    }


    public void panelOnClick()
    {



        scrollbar.interactable = false;
        panel.gameObject.GetComponent<Animator>().SetTrigger("FlyIn");
        content.GetComponent<Animator>().SetTrigger("FlyOut");
        //panel.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        //panel.gameObject.GetComponent<CanvasGroup>().interactable = true;
        //panel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        content.gameObject.GetComponent<CanvasGroup>().interactable = false;
        content.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        exitCatalogButton.interactable = false;
        exitPanelButton.gameObject.SetActive(true);

        

    }

    public void onExitButtonClick()
    {
        scrollbar.interactable = true;
        panel.gameObject.GetComponent<Animator>().SetTrigger("FlyOut");
        content.GetComponent<Animator>().SetTrigger("FlyIn");
        //panel.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        //panel.gameObject.GetComponent<CanvasGroup>().interactable = false;
        //panel.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        content.gameObject.GetComponent<CanvasGroup>().interactable = true;
        content.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        exitCatalogButton.interactable = true;
        gameObject.SetActive(false);
    }
}

