using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // public���� = ����Ƽ �ν����Ϳ��� �� �� �ְ�
    public TextMeshProUGUI bestscoreTitle;
    public TextMeshProUGUI bestscoreText;
    public TextMeshProUGUI scoreTitle;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI backtomainText;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ó��
        if (scoreText == null)
            Debug.LogError("score text is null");
        if (gameoverText == null)
            Debug.LogError("gameover text is null");

        // ���� ���� ���� gameoverText, backtomainText�� �ʿ� ����
        gameoverText.gameObject.SetActive(false);
        backtomainText.gameObject.SetActive(false);
    }

    public void SetGameOver()
    {
        gameoverText.gameObject.SetActive(true);
        backtomainText.gameObject.SetActive(true);
    }

    public void UpdateScore(int curscore)
    {
        scoreText.text = curscore.ToString();        
    }

    public void UpdateBestScore(int bestscore)
    {
        bestscoreText.text = bestscore.ToString();
    }
}
