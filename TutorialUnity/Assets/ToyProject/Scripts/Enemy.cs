using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �÷��̾��� ���� (���߾�)
    // ������ �������� �ȵǴ� ���� -> ���߾��� ��ġ�� ����ϴ� ��.
    // ���� �ൿ�� ����� �˰��� AI �ൿ ����

    public GameObject centerPoint;
    public float enemyMoveSpeed;
    public Rigidbody rigidbody;

    private Vector3 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        // ������ �ѹ��� �����ǰ� Enemy �� �������θ� �����̱� ������ (�Ѿ� ���ϱ�)
        //targetDirection = (playerObject.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy
        // ó�� �����Ҷ� �÷��̾� ������Ʈ ���� ��ġ�� ��� �̵��ϵ��� ����
        targetDirection = (centerPoint.transform.position - transform.position).normalized;
        rigidbody.AddForce(targetDirection * enemyMoveSpeed);
    }
}
