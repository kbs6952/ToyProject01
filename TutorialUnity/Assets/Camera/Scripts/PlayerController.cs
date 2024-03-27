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
        [Header("플레이어 매니저 스크립트")]
        [HideInInspector] public PlayerAnimatorManager animaotionManager;


        [Header("플레이어 입력 제어 변수")]

        [SerializeField] private float playerMoveSpeed;     // 플레이어의 기본 속도
        [SerializeField] private float runSpeed;            // 플레이어가 달릴 때의 속도
        [SerializeField] private float jumpForce;           // 플레이어의 점프력

        private CharacterController cCon;                   // Rigidbody 대신 Character에 물리 충돌 기능과 이동을 위한 컴포넌트, Start에서 초기화

        [Header("카메라 제어 변수")]
        [SerializeField] ThirdCamController thirdCam;
        [SerializeField] float smoothRotation = 100f;
        Quaternion targetRotation;

        [Header("점프 제어 변수")]
        [SerializeField] private float garavityModifier = 3f; // 플레이어가 땅에 떨어지는 
        [SerializeField] private Vector3 groundCheckPoint;  // 땅을 판별하기 위한 체크 포인트
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayer;

        private bool isGrounded;


        private float activeMoveSpeed;                      // 실제로 플레이어가 이동할 속력을 저장할 변수
        private Vector3 movement;

        [Header("애니메이터")]
        private Animator playerAnimator;


        // Rigidbody 클래스를 초기화 해주세요.

       
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
            
            // 1. Input 클래스를 이용하여 키보드 입력을 제어

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

          
            // 2. 키보드 Input과 입력 값을 확인하기 위한 변수 선언

            Vector3 moveInput = new Vector3(horizontal, 0, vertical).normalized;            // 키보드 입력값을 저장하는 벡터
            float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));    // 키보드로 상하좌우 키 한개만 입력을 하면 0보다 큰 값을 moveAmount에 저장한다.     

            // 3. 플레이어 캐릭터 이동할 방향을 지정할 변수 선언

            Vector3 moveDirection = thirdCam.transform.forward * moveInput.z + thirdCam.transform.right * moveInput.x;
            moveDirection.y = 0;


            // 4. 플레이어의 이동 속도를 다르게 해주는 코드 (달리기 기능)
            if (Input.GetKey(KeyCode.LeftShift))  // key Down : 누를 때 한번, Key : Key버튼을 떼기 전까지
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



            // 5. 점프를  하기 위한 계산식

            float yValue = movement.y;                      // 떨어지고 있는 y의 크기를 저장
            movement = moveDirection * activeMoveSpeed;     // 좌표에 이동할 x,0,z 벡터 값을 저장
            movement.y = yValue;

            // 다중 점프되는 문제점이 발생
            GroundCheck();



            // 점프키를 입력하여 점프 구현
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerAnimator.CrossFade("Jump", 0.2f);         // 두 번째 매개변수 : 현재 State에서 실행하고 싶은 애니메이션을 자동으로 Blend해주는 시간
                
                movement.y = jumpForce;
            }



            movement.y += Physics.gravity.y * garavityModifier * Time.deltaTime;



            //transform.position += moveDir * playerMoveSpeed * Time.deltaTime; // 프레임수와 상관없이 같은 시간에 같은 거리를 움직인다.
            cCon.Move(moveInput * activeMoveSpeed * Time.deltaTime);

            // 6. 
            if (moveAmount > 0)
            {
                targetRotation = Quaternion.LookRotation(moveDirection);

            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smoothRotation);
            cCon.Move(movement * Time.deltaTime);
            playerAnimator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);        // dampTime : 첫번째 변수(이전 값), 2번쨰 변수(변화시키고 싶은 값)

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
            player.canCombo = true;                                                     // canCombo True일 때만 콤보 어택을 할 수 있게 제어 변수 선언
        }
        private void HandleComboAttack()
        {
            //if (!player.canCombo) return; // 예외 사항 처리

            //// 콤보 어택 사용할 입력 키 설정

            //if (Input.GetButtonDown("0"))
            //{
            //    player.animator.SetTrigger("doAttack");
            //}

        }

    } 
}
