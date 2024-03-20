using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirdCamController : MonoBehaviour
{

    [Header("카메라 제어 변수")]
    [SerializeField] private Transform target;      // 카메라가 찍을 대상
    [SerializeField] private float camDistance;     // 대상과 카메라와의 거리
    [SerializeField] private float rotSpeed;        // 카메라가 회전하는 속도 크기
    [SerializeField] private int limitAngle;        // 카메라의 제한 각도
    [SerializeField] private bool inverseX;         // 마우스 위아래 반전 체크 
    [SerializeField] private bool inverseY;         // 마우스 좌우   반전 체크
    
    float rotationX;
    float rotationY;

    public Quaternion comLookRotation => Quaternion.Euler(0, rotationY, 0); 
    // Update is called once per frame
    void Update()
    {
        float invertXValue = (inverseX ? -1 : 1);
        float invertYValue = (inverseY ? -1 : 1);

        // 마우스의 입력 값을 받아준다.
        // 마우스를 위아래로 움직일 때마다 rotationX의 값이 변화되어 저장이 됩니다.
        // 상하 회전 구현
        rotationX -= Input.GetAxis("Mouse Y") * invertYValue * rotSpeed;     // 상하 회전에 대한 마우스 입력 값
        rotationX = Mathf.Clamp(rotationX, -limitAngle, limitAngle);

        // 좌우 회전 구현
        rotationY += Input.GetAxis("Mouse X") * invertXValue * rotSpeed;     // 좌우 회전에 대한 마우스 입력 값
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        transform.rotation = targetRotation;

        //카메라가 플레이어를 쫓아서 이동하는 로직
        Vector3 focusPosition = target.position;
        transform.position = focusPosition - (target.rotation * new Vector3(0, 0, camDistance));
    }
}
