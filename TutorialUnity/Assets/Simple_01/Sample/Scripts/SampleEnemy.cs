using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour
{
    [SerializeField] private GameObject centerPoint;
    [SerializeField] private float enemyMoveSpeed;
    private Rigidbody rigidbody;
    private Vector3 targetDirection;

    public GameObject CenterPoint
    {
        get => centerPoint;
        set => centerPoint = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // ������ �ѹ��� �����ǰ�. Enemy �� �������θ� �����̱� ������ (�Ѿ� ���ϱ�)
        //targetDirection = (playerObject.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy�� ���߾��� ��ġ�� ����ؼ� �̵��ϴ� ����
        targetDirection = (centerPoint.transform.position - transform.position).normalized;

        // Acceleration : �������� ���� �ް� ��������x
        // Force : �������� ���� �ް� ���Կ� ����o
        // Impuse : �������� ��o ��������o
        // VelocityChange : �������� ��o �������� x
        rigidbody.AddForce(targetDirection * enemyMoveSpeed,ForceMode.Force);


    }
}