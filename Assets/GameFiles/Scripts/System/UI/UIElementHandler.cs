using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIElementHandler : MonoBehaviour
{
    private static UIElementHandler _instance;
    public static UIElementHandler Instance => _instance;

    [Header("========== Text Elements ==========")]
    [SerializeField] private List<string> textId;
    [SerializeField] private List<Text> text;
    
    [Header("========== Button Elements ==========")]
    
    [SerializeField] private List<string> buttonId;
    [SerializeField] private List<Button> button;
    
    [Header("========== Panel Elements ==========")]
    [SerializeField] private List<string> panelId;
    [SerializeField] private List<GameObject> panel;
    
    [Header("========== Canvas Elements ==========")]
    
    [SerializeField] private List<string> canvasId;
    [SerializeField] private List<Canvas> canvas;
    
    [Header("========== Slider Elements ==========")]
    
    [SerializeField] private List<string> sliderId;
    [SerializeField] private List<Slider> slider;

    private void Awake()
    {
        _instance = this;
    }

    public Text GetText(string id)
    {
        // get index of the id and return text at this index
        print(SceneManager.GetActiveScene().name);
        var index = textId.FindIndex(x => x == id);
        return text[index];
    }
    public Button GetButton(string id)
    {
        var index = buttonId.FindIndex(x => x == id);
        return button[index];
    }
    public GameObject GetPanel(string id)
    {
        var index = panelId.FindIndex(x => x == id);
        return panel[index];
    }
    public Canvas GetCanvas(string id)
    {
        var index = canvasId.FindIndex(x => x == id);
        return canvas[index];
    }
    public Slider GetSlider(string id)
    {
        var index = sliderId.FindIndex(x => x == id);
        return slider[index];
    }
    
    public void SetButtonEvent(string id, UnityAction onButtonClick)
    {
        var index = buttonId.FindIndex(x => x.Equals(id)); // get Button
        button[index].onClick.AddListener(onButtonClick); // add a onClick event
    }
}
