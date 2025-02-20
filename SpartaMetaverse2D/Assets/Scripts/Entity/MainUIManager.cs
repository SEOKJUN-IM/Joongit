using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public TextMeshProUGUI bestscoreText;

    public void UpdateBestScore(int bestscore)
    {
        bestscoreText.text = bestscore.ToString();
    }
}
