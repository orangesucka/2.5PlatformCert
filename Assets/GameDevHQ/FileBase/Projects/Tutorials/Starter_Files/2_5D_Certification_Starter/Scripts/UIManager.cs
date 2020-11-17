using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text collectibleText;

    public void UpdateCollectiblesDisplayed(int collectible)
    {
        collectibleText.text = "Collectibles: " + collectible.ToString();
    }
}