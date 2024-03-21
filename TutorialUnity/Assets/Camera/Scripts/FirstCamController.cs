using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCamController : MonoBehaviour
{
    [SerializeField] private Transform viewPort;

    // 카메라 회전 제어를 위한 변수
    [SerializeField] private float mouseSensivity = 1f;
    [SerializeField] private int limitAngle = 60;   // 마우스의 각도를 제한하는 값
    private float verticalRot;                      // 오일러 회전 수치를 저장해두기 위한 값
    private Vector2 mouseInput;

    [SerializeField] private bool inversLook;       // true이면 마우스 상하 반전, false이면 정상
    // 1인칭 카메라를 플레이어의 자식으로 귀속시키지 않고 플레이어를 따라오게 하기 위해
    // 카메라를 변수로 가져온다
    [SerializeField] private Camera firstCam;       // 1인칭 카메라 게임오브젝트를 사용하기 위한 변수
     

    // Start is called before the first frame update
    void Start()
    {
        // 마우스 커서를 제한하는 파트
        Cursor.visible = false;             // 메뉴, 옵션 버튼을 클릭시 마우스 버튼이 보이게한다.
        Cursor.lockState = CursorLockMode.Locked;   // 마우스가 화면밖을 나가지 않음.
    }

    // Update is called once per frame
    void Update()
    {
        float inverseValue = inversLook ? -1 : 1;   // inverseValue - inverseLook Bool값에 따라 마우스 회전을 변경할 수 있게 된다.

        float rotationX = Input.GetAxisRaw("Mouse X");
        float rotationY = Input.GetAxisRaw("Mouse Y") * inverseValue;

        // 카메라 좌우 회전을 위한 변수
        mouseInput = new Vector2(rotationX, rotationY) * mouseSensivity;

        // 좌우 회전
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x,
            transform.rotation.eulerAngles.z);

        // 상하 회전

        verticalRot -= mouseInput.y;
        verticalRot = Mathf.Clamp(verticalRot, -limitAngle, limitAngle);    // 첫번째 인자로 들어간 값이 최소 최대 값을 넘어서지 않게 해준다.


        viewPort.rotation = Quaternion.Euler(verticalRot,
            viewPort.rotation.eulerAngles.y,
            viewPort.rotation.eulerAngles.z);
    }
    private void LateUpdate()       // playerController의 Update문에서 플레이어의 이동이 적용. firstCamController 카메라의 회전이 적용
    {
        firstCam.transform.SetPositionAndRotation(viewPort.position, viewPort.rotation);

        // * 선형 보간법으로 회전을 부드럽게 적용해보기 *
    }
}
