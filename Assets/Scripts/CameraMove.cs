using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Transform: 위치, 회전, 크기를 결정하는 컴포넌트
    // transform: 오브젝트에 할당된 Transform 컴포넌트, GetComponent<Transform>()와 같다. 값을 담고 있는 객체이다.
    Transform playerTransform;
    Vector3 Offset;

    void Awake()
    {
        // FindGameObjectWithTag(): 주어진 태그로 오브젝트 검색
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Player의 위치, 회전, 크기 컴포넌트
        Offset = transform.position - playerTransform.position; // 메인 카메라와 플레이어 위치 백터 차이
    }

    // Update 뒤에 실행됨, UI/카메라 이동에 관한 것이 이루어짐
    void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;
    }
}
