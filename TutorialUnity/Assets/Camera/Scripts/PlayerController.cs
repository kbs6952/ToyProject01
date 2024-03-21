using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace CameraSetting
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("�÷��̾� �Է� ���� ����")]

        [SerializeField] private float playerMoveSpeed;     // �÷��̾��� �⺻ �ӵ�
        [SerializeField] private float runSpeed;            // �÷��̾ �޸� ���� �ӵ�
        [SerializeField] private float jumpForce;           // �÷��̾��� ������

        private CharacterController cCon;                   // Rigidbody ��� Character�� ���� �浹 ��ɰ� �̵��� ���� ������Ʈ, Start���� �ʱ�ȭ

        [Header("ī�޶� ���� ����")]
        [SerializeField] ThirdCamController thirdCam;
        [SerializeField] float smoothRotation = 100f;
        Quaternion targetRotation;

        [Header("���� ���� ����")]
        [SerializeField] private float garavityModifier = 3f; // �÷��̾ ���� �������� 
        [SerializeField] private Vector3 groundCheckPoint;  // ���� �Ǻ��ϱ� ���� üũ ����Ʈ
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayer;

        private bool isGrounded;


        private float activeMoveSpeed;                      // ������ �÷��̾ �̵��� �ӷ��� ������ ����
        private Vector3 movement;

        // Rigidbody Ŭ������ �ʱ�ȭ ���ּ���.

       
        // Start is called before the first frame update
        void Start()
        {
            cCon = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            // 1. Input Ŭ������ �̿��Ͽ� Ű���� �Է��� ����

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 2. Ű���� Input�� �Է� ���� Ȯ���ϱ� ���� ���� ����

            Vector3 moveInput = new Vector3(horizontal, 0, vertical).normalized;            // Ű���� �Է°��� �����ϴ� ����
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal)+Mathf.Abs(vertical));    // Ű����� �����¿� Ű �Ѱ��� �Է��� �ϸ� 0���� ū ���� moveAmount�� �����Ѵ�.     

            // 3. �÷��̾� ĳ���� �̵��� ������ ������ ���� ����

            Vector3 moveDirection = thirdCam.comLookRotation * moveInput;
          

            // 4. �÷��̾��� �̵� �ӵ��� �ٸ��� ���ִ� �ڵ� (�޸��� ���)
            if (Input.GetKey(KeyCode.LeftShift))  // key Down : ���� �� �ѹ�, Key : Key��ư�� ���� ������
                activeMoveSpeed = runSpeed;
            else
                activeMoveSpeed = playerMoveSpeed;


            // 5. ������  �ϱ� ���� ����

            float yValue = movement.y;                      // �������� �ִ� y�� ũ�⸦ ����
            movement = moveDirection * activeMoveSpeed;     // ��ǥ�� �̵��� x,0,z ���� ���� ����
            movement.y = yValue;

            // ���� �����Ǵ� �������� �߻�
            GroundCheck();

            if (cCon.isGrounded)
            {
                movement.y = 0;                                 // ������ �����϶� y�� 
                Debug.Log("�÷��̾ ���� �ִ� �����Դϴ�");
            }

            // ����Ű�� �Է��Ͽ� ���� ����
            if (Input.GetButtonDown("Jump") && isGrounded)
            { 
                movement.y = jumpForce;
            }    

            

            movement.y += Physics.gravity.y * garavityModifier* Time.deltaTime;
            
            

            //transform.position += moveDir * playerMoveSpeed * Time.deltaTime; // �����Ӽ��� ������� ���� �ð��� ���� �Ÿ��� �����δ�.
            cCon.Move(moveInput * activeMoveSpeed * Time.deltaTime);

            // 6. 
            if(moveAmount > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
            cCon.Move(movement * Time.deltaTime);
        }

        private void GroundCheck()
        {
            isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius, groundLayer);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawSphere(transform.TransformPoint(groundCheckPoint), groundCheckRadius);
        }
    } 
}
