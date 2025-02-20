using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // public으로 = 유니티 인스펙터에서 쓸 수 있게
    public TextMeshProUGUI bestscoreTitle;
    public TextMeshProUGUI bestscoreText;
    public TextMeshProUGUI scoreTitle;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI backtomainText;

    // Start is called before the first frame update
    void Start()
    {
        // 예외 처리
        if (scoreText == null)
            Debug.LogError("score text is null");
        if (gameoverText == null)
            Debug.LogError("gameover text is null");

        // 게임 켰을 때는 gameoverText, backtomainText가 필요 없음
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
