using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump;
    AudioSource audio;
    Rigidbody rigid;

    // 게임이 시작되기 전에 모든 변수와 게임 상태를 초기화하기 위해 호출됨
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 1단 점프만 가능하게 함
        if (Input.GetButtonDown("Jump") && !isJump) {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    // 프레임을 기반으로 호출되는 Update와 달리, Fixed Timestep에 설정된 값에 따라 일정한 간격으로 호출됨
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); // 왼오 이동
        float v = Input.GetAxisRaw("Vertical"); // 상하 이동
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void goToFirstStage()
    {
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item") // 아이템 먹었을 때
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false); // 플레이어랑 아이템이랑 닿으면, 아이템 사라짐
            manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish") // 결승점 도착
        {
            if (itemCount == manager.totalItemCount) // 스테이지 아이템 모두 먹음
            {
                // Game Clear & Next Stage
                if (manager.stage == 2) // 마지막 스테이지
                {
                    manager.clearFlag = true;
                    manager.ClearGame(); // 게임 클리어 안내문 띄움
                    Invoke(nameof(goToFirstStage), 5f); // 5초뒤 첫 번째 스테이지로 이동
                }
                else SceneManager.LoadScene(manager.stage + 1);
            }
            else
            {
                // Restart
                SceneManager.LoadScene(manager.stage); // LoadScene(): 주어진 장면을 불러오는 함수
            }
        }
    }
}
