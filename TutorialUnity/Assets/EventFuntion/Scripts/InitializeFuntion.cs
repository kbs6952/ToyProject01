using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialrised : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake �Լ��� �����");
    }
    private void OnEnable()
    {
        Debug.Log("enable �Լ��� �����");

    }

    // start is called before the first frame update
    void Start()
    {
        Debug.Log("start �Լ��� �����");
    }

    // update is called once per frame
    void Update()
    {

    }
}
