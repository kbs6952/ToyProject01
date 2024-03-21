using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCamController : MonoBehaviour
{
    [SerializeField] private Transform viewPort;

    // ī�޶� ȸ�� ��� ���� ����
    [SerializeField] private float mouseSensivity = 1f;
    [SerializeField] private int limitAngle = 60;   // ���콺�� ������ �����ϴ� ��
    private float verticalRot;                      // ���Ϸ� ȸ�� ��ġ�� �����صα� ���� ��
    private Vector2 mouseInput;

    [SerializeField] private bool inversLook;       // true�̸� ���콺 ���� ����, false�̸� ����
    // 1��Ī ī�޶� �÷��̾��� �ڽ����� �ͼӽ�Ű�� �ʰ� �÷��̾ ������� �ϱ� ����
    // ī�޶� ������ �����´�
    [SerializeField] private Camera firstCam;       // 1��Ī ī�޶� ���ӿ�����Ʈ�� ����ϱ� ���� ����
     

    // Start is called before the first frame update
    void Start()
    {
        // ���콺 Ŀ���� �����ϴ� ��Ʈ
        Cursor.visible = false;             // �޴�, �ɼ� ��ư�� Ŭ���� ���콺 ��ư�� ���̰��Ѵ�.
        Cursor.lockState = CursorLockMode.Locked;   // ���콺�� ȭ����� ������ ����.
    }

    // Update is called once per frame
    void Update()
    {
        float inverseValue = inversLook ? -1 : 1;   // inverseValue - inverseLook Bool���� ���� ���콺 ȸ���� ������ �� �ְ� �ȴ�.

        float rotationX = Input.GetAxisRaw("Mouse X");
        float rotationY = Input.GetAxisRaw("Mouse Y") * inverseValue;

        // ī�޶� �¿� ȸ���� ���� ����
        mouseInput = new Vector2(rotationX, rotationY) * mouseSensivity;

        // �¿� ȸ��
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseInput.x,
            transform.rotation.eulerAngles.z);

        // ���� ȸ��

        verticalRot -= mouseInput.y;
        verticalRot = Mathf.Clamp(verticalRot, -limitAngle, limitAngle);    // ù��° ���ڷ� �� ���� �ּ� �ִ� ���� �Ѿ�� �ʰ� ���ش�.


        viewPort.rotation = Quaternion.Euler(verticalRot,
            viewPort.rotation.eulerAngles.y,
            viewPort.rotation.eulerAngles.z);
    }
    private void LateUpdate()       // playerController�� Update������ �÷��̾��� �̵��� ����. firstCamController ī�޶��� ȸ���� ����
    {
        firstCam.transform.SetPositionAndRotation(viewPort.position, viewPort.rotation);

        // * ���� ���������� ȸ���� �ε巴�� �����غ��� *
    }
}
