using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamController : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTr;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position-playerTr.position; // 카메라 - > 플레이어를 바라보는 방향과 크기
    }
    // PlayerController Update에 이동시키고, LateUpdate 카메라를 움직여 준다.

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTr.position + offset; // 카메라의 위치 = 플레이어가 이동한 위치 + 카메라와 플레이어가 고정되어야할 방향과 거리

        offset = playerTr.position - transform.position; // 플레이어가 이동함에 따라 변화한 offset을 다시 갱신해준다.
    }
}
