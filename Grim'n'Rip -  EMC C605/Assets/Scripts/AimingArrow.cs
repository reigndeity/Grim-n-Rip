using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingArrow : MonoBehaviour
{
    [SerializeField] Camera cameraMain;

    void Start()
    {
        cameraMain = FindObjectOfType<Camera>();
    }
    void Update()
    {
        transform.rotation = cameraMain.transform.rotation;
    }
}
