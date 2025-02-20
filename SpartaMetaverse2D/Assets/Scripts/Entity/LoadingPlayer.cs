using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlayer : MonoBehaviour
{
    SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = SceneChanger.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            sceneChanger.GoMiniGame();
    }
}
