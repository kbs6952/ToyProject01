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
        PlayerMovementManager player;
        [Header("�÷��̾� �Ŵ��� ��ũ��Ʈ")]
        [HideInInspector] public PlayerAnimatorManager animaotionManager;


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

        [Header("�ִϸ�����")]
        private Animator playerAnimator;


        // Rigidbody Ŭ������ �ʱ�ȭ ���ּ���.

       
        // Start is called before the first frame update
        void Start()
        {
            cCon = GetComponent<CharacterController>();
            playerAnimator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
           
            HandleActionInput();
            HandleComboAttack();
            HandleMovement();
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
        private void HandleMovement()
        {
            
            // 1. Input Ŭ������ �̿��Ͽ� Ű���� �Է��� ����

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

          
            // 2. Ű���� Input�� �Է� ���� Ȯ���ϱ� ���� ���� ����

            Vector3 moveInput = new Vector3(horizontal, 0, vertical).normalized;            // Ű���� �Է°��� �����ϴ� ����
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));    // Ű����� �����¿� Ű �Ѱ��� �Է��� �ϸ� 0���� ū ���� moveAmount�� �����Ѵ�.     

            // 3. �÷��̾� ĳ���� �̵��� ������ ������ ���� ����

            Vector3 moveDirection = thirdCam.transform.forward * moveInput.z + thirdCam.transform.right * moveInput.x;
            moveDirection.y = 0;


            // 4. �÷��̾��� �̵� �ӵ��� �ٸ��� ���ִ� �ڵ� (�޸��� ���)
            if (Input.GetKey(KeyCode.LeftShift))  // key Down : ���� �� �ѹ�, Key : Key��ư�� ���� ������
            {
                activeMoveSpeed = runSpeed;
                moveAmount++;
                playerAnimator.SetBool("IsRun", true);
            }
            else
            {
                activeMoveSpeed = playerMoveSpeed;
                playerAnimator.SetBool("IsRun", false);
            }



            // 5. ������  �ϱ� ���� ����

            float yValue = movement.y;                      // �������� �ִ� y�� ũ�⸦ ����
            movement = moveDirection * activeMoveSpeed;     // ��ǥ�� �̵��� x,0,z ���� ���� ����
            movement.y = yValue;

            // ���� �����Ǵ� �������� �߻�
            GroundCheck();



            // ����Ű�� �Է��Ͽ� ���� ����
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerAnimator.CrossFade("Jump", 0.2f);         // �� ��° �Ű����� : ���� State���� �����ϰ� ���� �ִϸ��̼��� �ڵ����� Blend���ִ� �ð�
                
                movement.y = jumpForce;
            }



            movement.y += Physics.gravity.y * garavityModifier * Time.deltaTime;



            //transform.position += moveDir * playerMoveSpeed * Time.deltaTime; // �����Ӽ��� ������� ���� �ð��� ���� �Ÿ��� �����δ�.
            cCon.Move(moveInput * activeMoveSpeed * Time.deltaTime);

            // 6. 
            if (moveAmount > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDirection);

            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation);
            cCon.Move(movement * Time.deltaTime);
            playerAnimator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);        // dampTime : ù��° ����(���� ��), 2���� ����(��ȭ��Ű�� ���� ��)

        }

        private void HandleActionInput()
        {
            if(Input.GetMouseButtonDown(0))
            {
                HandleAttackAction();
            }
        }
        private void HandleAttackAction()
        {
            player.playerAnimatorManager.PlayerTargetActionAnimation("ATK0", true);
            player.canCombo = true;                                                     // canCombo True�� ���� �޺� ������ �� �� �ְ� ���� ���� ����
        }
        private void HandleComboAttack()
        {
            //if (!player.canCombo) return; // ���� ���� ó��

            //// �޺� ���� ����� �Է� Ű ����

            //if (Input.GetButtonDown("0"))
            //{
            //    player.animator.SetTrigger("doAttack");
            //}

        }

    } 
}
