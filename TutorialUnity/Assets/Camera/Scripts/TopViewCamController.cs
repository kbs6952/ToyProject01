using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamController : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform playerTr;
    [SerializeField] float smoothValue = 5f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTr.position; // ī�޶� - > �÷��̾ �ٶ󺸴� ����� ũ��
    }
    // PlayerController Update�� �̵���Ű��, LateUpdate ī�޶� ������ �ش�.

    // Update is called once per frame
    void Update()
    {
        #region ī�޶� �÷��̾�� ���� �ӵ��� �̵���
        //transform.position = playerTr.position + offset; // ī�޶��� ��ġ = �÷��̾ �̵��� ��ġ + ī�޶�� �÷��̾ �����Ǿ���� ����� �Ÿ�

        //offset = transform.position - playerTr.position; // �÷��̾ �̵��Կ� ���� ��ȭ�� offset�� �ٽ� �������ش�. 
        #endregion

        // ���� ������ ����� ī�޶� �̵�

        // �� ���� ���� �־����� �� 0 ~ 1 Percent�� �� ������ ���� �����ϴ� ����̴�.

        #region ���������� ����� ī�޶� �̵�
        Vector3 targetCamPos = playerTr.position + offset;      // (Update�� ������) ī�޶� ���������� �����ؾ��� ��ġ�Դϴ�.
        // 1�� �Ű� ���� : ī�޶� �̵��ϱ� �� ��ġ, 2�� �Ű����� : Update�� ���� �� ���������� �̵��� ��ġ, 3�� �Ű� ���� : a�� b�� �Ÿ� ������ percent�� ��Ÿ�� ��
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothValue * Time.deltaTime);   
        #endregion                    // Vector ����� ũ�⸦ ���� �����͸� ������ �ִµ�, ������ ������ ä�� ũ�⸸ ���ݾ� õõ�� �̵����� ���̴ϴ�.
    }
    
}
