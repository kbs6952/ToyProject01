using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionable
{
    public void CollideWithPlayer(); // Player(공)과 부딪힌 객체가 특정 방향으로 날라가는 기능을 인터페이스 구현하겠다.
}
public class Enemy : MonoBehaviour, ICollisionable
{
    // 플레이어의 방향 (정중앙)
    // 밖으로 떨어지면 안되는 게임 -> 정중앙의 위치를 고수하는 것.
    // 적의 행동을 만드는 알고리즘 AI 행동 패턴

    public GameObject centerPoint;
    public float enemyMoveSpeed;
    public Rigidbody rigidbody;

    private Vector3 targetDirection;

    public void CollideWithPlayer()
    {
        // 플레이어와 충돌했을 때 객체가 날라가는 로직을 작성해주면 됩니다.
        Debug.Log("Collide인터페이스가 호출됨!");
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // 방향이 한번만 결정되고 Enemy 그 방향으로만 움직이기 때문에 (총알 피하기)
        //targetDirection = (playerObject.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy
        // 처음 시작할때 플레이어 오브젝트 생성 위치로 계속 이동하도록 설정
        targetDirection = (centerPoint.transform.position - transform.position).normalized;
        rigidbody.AddForce(targetDirection * enemyMoveSpeed);
    }
}
