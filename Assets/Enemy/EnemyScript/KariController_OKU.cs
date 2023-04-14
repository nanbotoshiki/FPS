using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KariController_OKU : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(
        0,
        Input.GetAxis("Horizontal") * 90 * Time.deltaTime,
        0
        );

        transform.Translate(
        0,
        0,
        Input.GetAxis("Vertical") * 5.0f * Time.deltaTime);
    }
}
