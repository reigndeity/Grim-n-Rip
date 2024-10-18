using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] Camera cameraMain;
    void Start()
    {
        cameraMain = FindObjectOfType<Camera>();
        Invoke("DestroyFloatingText", 0.15f);
    }


    void DestroyFloatingText()
    {
        Destroy(gameObject);
    }


    void Update()
    {
        transform.rotation = cameraMain.transform.rotation;
    }
}
