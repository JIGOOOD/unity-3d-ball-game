using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; // 스테이지 아이템 수
    public int stage; // 스테이지 수
    public Text stageCountText; // 스테이지 아이템 수
    public Text playerCountText; // 플레이어가 획득한 아이템 수
    public bool clearFlag; // 게임 클리어 여부
    GameObject[] clear; // 클리어 Tag를 가진 Objects

    void Awake()
    {
        clearFlag = false;
        clear = GameObject.FindGameObjectsWithTag("Clear");
        stageCountText.text = "/" + totalItemCount;
    }

    // 게임 클리어 안내문을 비활성화
    private void Start()
    {
        for (int i = 0; i < clear.Length; i++)
            clear[i].SetActive(false);
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }
    
    // 게임 클리어 안내문 활성화
    public void ClearGame()
    {
        for (int i = 0; i < clear.Length; i++)
            clear[i].SetActive(true);
    }

    // Player가 낙하했을 때, 다시 현 스테이지로 복귀 (마지막 스테이지에 도달했을 경우, 무효)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !clearFlag)
            SceneManager.LoadScene(stage);
    }
}
