using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class FlappyManager : MonoBehaviour
{
    // 싱글턴 패턴 만들 때 가장 기본적인 형태
    public static FlappyManager Instance;

    // 점수 만들어주기

    public int currentScore = 0;
    public int bestScore;

    GameManager gameManager;
    // UI매니저에 접근할 수 있게 생성
    UIManager uiManager;
    // 혹시나 외부에서 UI매니저를 써야될 수도 있기 때문에 하나 만들어두기
    public UIManager UIManager { get { return uiManager; } }

    private void Awake() // 가장 최초의 객체를 설정해주는 작업
    {
        Instance = this;
        
        gameManager = FindObjectOfType<GameManager>();
        // UI매니저 찾아오기
        uiManager = FindObjectOfType<UIManager>();
        
        uiManager.UpdateBestScore(gameManager.flappybestscore);
    }
    // 이렇게 함으로써 외부에서 Instance라고 하는 프로퍼티를 통해서 하나의 객체를 쉽게 접근할 수가 있다.

    public void AddScore(int score)
    {
        currentScore += score;

        if (currentScore >= gameManager.flappybestscore)
            gameManager.flappybestscore = currentScore;

        // 게임매니저 점수 올라가는 메서드 안에서 UI매니저의 UpdateScore 메서드 호출
        uiManager.UpdateScore(currentScore);
        uiManager.UpdateBestScore(gameManager.flappybestscore);
    }

    public void GameOver()
    {
        uiManager.SetGameOver(); // UI매니저의 게임오버 메서드 호출        
    }

    public void SetBestScore()
    {
        if (currentScore >= gameManager.flappybestscore)
        {             
            bestScore = currentScore;
            PlayerPrefs.SetInt(gameManager.bestScoreKey, bestScore);
        }
        else if (currentScore < gameManager.flappybestscore)
        {            
            bestScore = gameManager.flappybestscore;
            PlayerPrefs.SetInt(gameManager.bestScoreKey, bestScore);
        }
    }
}
