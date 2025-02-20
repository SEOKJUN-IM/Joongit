using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class FlappyManager : MonoBehaviour
{
    // �̱��� ���� ���� �� ���� �⺻���� ����
    public static FlappyManager Instance;

    // ���� ������ֱ�

    public int currentScore = 0;
    public int bestScore;

    GameManager gameManager;
    // UI�Ŵ����� ������ �� �ְ� ����
    UIManager uiManager;
    // Ȥ�ó� �ܺο��� UI�Ŵ����� ��ߵ� ���� �ֱ� ������ �ϳ� �����α�
    public UIManager UIManager { get { return uiManager; } }

    private void Awake() // ���� ������ ��ü�� �������ִ� �۾�
    {
        Instance = this;
        
        gameManager = FindObjectOfType<GameManager>();
        // UI�Ŵ��� ã�ƿ���
        uiManager = FindObjectOfType<UIManager>();
        
        uiManager.UpdateBestScore(gameManager.flappybestscore);
    }
    // �̷��� �����ν� �ܺο��� Instance��� �ϴ� ������Ƽ�� ���ؼ� �ϳ��� ��ü�� ���� ������ ���� �ִ�.

    public void AddScore(int score)
    {
        currentScore += score;

        if (currentScore >= gameManager.flappybestscore)
            gameManager.flappybestscore = currentScore;

        // ���ӸŴ��� ���� �ö󰡴� �޼��� �ȿ��� UI�Ŵ����� UpdateScore �޼��� ȣ��
        uiManager.UpdateScore(currentScore);
        uiManager.UpdateBestScore(gameManager.flappybestscore);
    }

    public void GameOver()
    {
        uiManager.SetGameOver(); // UI�Ŵ����� ���ӿ��� �޼��� ȣ��        
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
