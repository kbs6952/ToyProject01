using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFuntion : MonoBehaviour
{
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update�Լ��� ȣ��");
    }
    void LateUpdate()
    {
        Debug.Log("LateUpdate�Լ��� ȣ��");
    }
    void FixedUpdate()
    {
        Debug.Log("FixedUpdatee�Լ��� ȣ��");

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(0,0,0),new Vector3(1,1,1));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(0, 0, 5), 10f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(new Vector3(0, 0, -5), new Vector3(1, 2, 5));
    }
    private void OnApplicationQuit()
    {
        Debug.Log("�� ����!");
    }
    private void OnDestroy()
    {
        Debug.Log("�� ����!");
    }
    private void OnDisable()
    {
        Debug.Log("��������!");

    }
}
