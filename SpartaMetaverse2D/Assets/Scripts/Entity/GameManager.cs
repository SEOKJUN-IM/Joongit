using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ���� ���� �� ���� �⺻���� ����
    public static GameManager Instance;
        
    public int flappybestscore;    
    public string bestScoreKey;

    // UI�Ŵ����� ������ �� �ְ� ����
    UIManager uiManager;
    // Ȥ�ó� �ܺο��� UI�Ŵ����� ��ߵ� ���� �ֱ� ������ �ϳ� �����α�
    public UIManager UIManager { get { return uiManager; } }

    MainUIManager mainUIManager;
    public MainUIManager MainUIManager { get { return mainUIManager; } }

    private void Awake() // ���� ������ ��ü�� �������ִ� �۾�
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("gameManager On");
            DontDestroyOnLoad(gameObject);
            // UI�Ŵ��� ã�ƿ���
            uiManager = FindObjectOfType<UIManager>();
            mainUIManager = FindObjectOfType<MainUIManager>();

            flappybestscore = PlayerPrefs.GetInt(bestScoreKey);
            Debug.Log($"�ְ� ����: {flappybestscore}");
            mainUIManager.UpdateBestScore(flappybestscore);
        }
        else
        {
            Debug.Log("Two gameManager! One Destroy");
            Destroy(gameObject);
        }        
    }
    // �̷��� �����ν� �ܺο��� Instance��� �ϴ� ������Ƽ�� ���ؼ� �ϳ��� ��ü�� ���� ������ ���� �ִ�.
}
