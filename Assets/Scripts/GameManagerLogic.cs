using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount; // �������� ������ ��
    public int stage; // �������� ��
    public Text stageCountText; // �������� ������ ��
    public Text playerCountText; // �÷��̾ ȹ���� ������ ��
    public bool clearFlag; // ���� Ŭ���� ����
    GameObject[] clear; // Ŭ���� Tag�� ���� Objects

    void Awake()
    {
        clearFlag = false;
        clear = GameObject.FindGameObjectsWithTag("Clear");
        stageCountText.text = "/" + totalItemCount;
    }

    // ���� Ŭ���� �ȳ����� ��Ȱ��ȭ
    private void Start()
    {
        for (int i = 0; i < clear.Length; i++)
            clear[i].SetActive(false);
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }
    
    // ���� Ŭ���� �ȳ��� Ȱ��ȭ
    public void ClearGame()
    {
        for (int i = 0; i < clear.Length; i++)
            clear[i].SetActive(true);
    }

    // Player�� �������� ��, �ٽ� �� ���������� ���� (������ ���������� �������� ���, ��ȿ)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !clearFlag)
            SceneManager.LoadScene(stage);
    }
}
