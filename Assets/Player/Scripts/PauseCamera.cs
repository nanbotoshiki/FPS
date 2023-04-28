using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCamera : MonoBehaviour
{ 
    private Vector3 previousPosition; // 前フレームのカメラ位置を記憶するための変数
    private Quaternion previousRotation; // 前フレームのカメラ回転を記憶するための変数
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // 毎フレーム呼び出される関数
    void Update()
    {
        // GameManagerのisPauseがtrueの場合、カメラの位置と回転を前フレームの値に戻す
        if (gameManager.isPause)
        {
            transform.position = previousPosition;
            transform.rotation = previousRotation;
        }
        // GameManagerのisPauseがfalseの場合、カメラの位置と回転を更新する
        /*
        else
        {
            previousPosition = transform.position;
            previousRotation = transform.rotation;
        }
        */
    }
}
