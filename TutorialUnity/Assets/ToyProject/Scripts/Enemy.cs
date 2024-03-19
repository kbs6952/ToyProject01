using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionable
{
    public void CollideWithPlayer(); // Player(��)�� �ε��� ��ü�� Ư�� �������� ���󰡴� ����� �������̽� �����ϰڴ�.
}
public class Enemy : MonoBehaviour, ICollisionable
{
    // �÷��̾��� ���� (���߾�)
    // ������ �������� �ȵǴ� ���� -> ���߾��� ��ġ�� ����ϴ� ��.
    // ���� �ൿ�� ����� �˰��� AI �ൿ ����

    public GameObject centerPoint;
    public float enemyMoveSpeed;
    public Rigidbody rigidbody;

    private Vector3 targetDirection;

    public void CollideWithPlayer()
    {
        // �÷��̾�� �浹���� �� ��ü�� ���󰡴� ������ �ۼ����ָ� �˴ϴ�.
        Debug.Log("Collide�������̽��� ȣ���!");
    }

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
