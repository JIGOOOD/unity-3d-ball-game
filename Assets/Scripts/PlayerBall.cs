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

    // ������ ���۵Ǳ� ���� ��� ������ ���� ���¸� �ʱ�ȭ�ϱ� ���� ȣ���
    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 1�� ������ �����ϰ� ��
        if (Input.GetButtonDown("Jump") && !isJump) {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }
    // �������� ������� ȣ��Ǵ� Update�� �޸�, Fixed Timestep�� ������ ���� ���� ������ �������� ȣ���
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); // �޿� �̵�
        float v = Input.GetAxisRaw("Vertical"); // ���� �̵�
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
        if (other.tag == "Item") // ������ �Ծ��� ��
        {
            itemCount++;
            audio.Play();
            other.gameObject.SetActive(false); // �÷��̾�� �������̶� ������, ������ �����
            manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish") // ����� ����
        {
            if (itemCount == manager.totalItemCount) // �������� ������ ��� ����
            {
                // Game Clear & Next Stage
                if (manager.stage == 2) // ������ ��������
                {
                    manager.clearFlag = true;
                    manager.ClearGame(); // ���� Ŭ���� �ȳ��� ���
                    Invoke(nameof(goToFirstStage), 5f); // 5�ʵ� ù ��° ���������� �̵�
                }
                else SceneManager.LoadScene(manager.stage + 1);
            }
            else
            {
                // Restart
                SceneManager.LoadScene(manager.stage); // LoadScene(): �־��� ����� �ҷ����� �Լ�
            }
        }
    }
}
