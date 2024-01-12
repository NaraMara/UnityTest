using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HUD : MonoBehaviour
{
    private int _coinCounter = 0;
    private Label _coinCounterLabel;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _coinCounterLabel = root.Q<Label>("CoinCounterLabel");
        UpdateHUD();
    }
    private void UpdateHUD()
    {
        _coinCounterLabel.text = _coinCounter.ToString();   
    }
    public void AddCoin()
    {
        _coinCounter++;
        UpdateHUD(); 
    }
}
