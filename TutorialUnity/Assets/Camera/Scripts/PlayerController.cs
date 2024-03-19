using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CameraSetting
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float playerMoveSpeed;
        private CharacterController cCon;
        // Rigidbody 클래스를 초기화 해주세요.

       
        // Start is called before the first frame update
        void Start()
        {
            cCon = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;

            // Rigidbody 초기화한 변수를 사용하고 Addforce 메소드를 이용해 플레이어를 움직여 보세요.
            //Rigidbody rigidbody = GetComponent<Rigidbody>();
            //rigidbody.AddForce(moveDir * playerMoveSpeed * Time.deltaTime);

            // 현재 위치 + 속도 + 가속도 = 이동거리

            //transform.position += moveDir * playerMoveSpeed * Time.deltaTime; // 프레임수와 상관없이 같은 시간에 같은 거리를 움직인다.
            cCon.Move(moveDir * playerMoveSpeed * Time.deltaTime);

        }
    } 
}
