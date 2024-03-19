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
        // Rigidbody Ŭ������ �ʱ�ȭ ���ּ���.

       
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

            // Rigidbody �ʱ�ȭ�� ������ ����ϰ� Addforce �޼ҵ带 �̿��� �÷��̾ ������ ������.
            //Rigidbody rigidbody = GetComponent<Rigidbody>();
            //rigidbody.AddForce(moveDir * playerMoveSpeed * Time.deltaTime);

            // ���� ��ġ + �ӵ� + ���ӵ� = �̵��Ÿ�

            //transform.position += moveDir * playerMoveSpeed * Time.deltaTime; // �����Ӽ��� ������� ���� �ð��� ���� �Ÿ��� �����δ�.
            cCon.Move(moveDir * playerMoveSpeed * Time.deltaTime);

        }
    } 
}
