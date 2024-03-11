using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float playerSpeed = 2f;
    public Rigidbody playerRigid;

    public GameObject centerPointObject;

    // Start is called before the first frame update

    // �����Ҷ� �ѹ��� ����
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    // ��� �ݺ� (������ �帧)
    void Update()
    {
        var vartical = Input.GetAxis("Vertical");
        playerRigid.AddForce(centerPointObject.transform.forward * playerSpeed*vartical);
    }
}
