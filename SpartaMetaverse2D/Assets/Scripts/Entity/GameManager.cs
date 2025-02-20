using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글턴 패턴 만들 때 가장 기본적인 형태
    public static GameManager Instance;
        
    public int flappybestscore;    
    public string bestScoreKey;

    // UI매니저에 접근할 수 있게 생성
    UIManager uiManager;
    // 혹시나 외부에서 UI매니저를 써야될 수도 있기 때문에 하나 만들어두기
    public UIManager UIManager { get { return uiManager; } }

    MainUIManager mainUIManager;
    public MainUIManager MainUIManager { get { return mainUIManager; } }

    private void Awake() // 가장 최초의 객체를 설정해주는 작업
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("gameManager On");
            DontDestroyOnLoad(gameObject);
            // UI매니저 찾아오기
            uiManager = FindObjectOfType<UIManager>();
            mainUIManager = FindObjectOfType<MainUIManager>();

            flappybestscore = PlayerPrefs.GetInt(bestScoreKey);
            Debug.Log($"최고 점수: {flappybestscore}");
            mainUIManager.UpdateBestScore(flappybestscore);
        }
        else
        {
            Debug.Log("Two gameManager! One Destroy");
            Destroy(gameObject);
        }        
    }
    // 이렇게 함으로써 외부에서 Instance라고 하는 프로퍼티를 통해서 하나의 객체를 쉽게 접근할 수가 있다.
}
