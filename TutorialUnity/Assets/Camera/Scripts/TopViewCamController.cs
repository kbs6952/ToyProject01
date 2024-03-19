using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamController : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTr;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position-playerTr.position; // ī�޶� - > �÷��̾ �ٶ󺸴� ����� ũ��
    }
    // PlayerController Update�� �̵���Ű��, LateUpdate ī�޶� ������ �ش�.

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTr.position + offset; // ī�޶��� ��ġ = �÷��̾ �̵��� ��ġ + ī�޶�� �÷��̾ �����Ǿ���� ����� �Ÿ�

        offset = playerTr.position - transform.position; // �÷��̾ �̵��Կ� ���� ��ȭ�� offset�� �ٽ� �������ش�.
    }
}
