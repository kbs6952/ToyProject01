using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialrised : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake 함수가 실행됨");
    }
    private void OnEnable()
    {
        Debug.Log("enable 함수가 실행됨");

    }

    // start is called before the first frame update
    void Start()
    {
        Debug.Log("start 함수가 실행됨");
    }

    // update is called once per frame
    void Update()
    {

    }
}
