using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text sphereText;

    public void UpdateSpheresDisplay(int spheres)
    {
        sphereText.text = "Spheres: " + spheres.ToString();
    }
}
