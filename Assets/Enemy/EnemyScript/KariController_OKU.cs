using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KariController_OKU : MonoBehaviour
{
/*    CharacterController cc;*/

    void Start()
    {
/*        //ƒLƒƒƒ‰ƒRƒ“æ“¾
        cc = GetComponent<CharacterController>();*/
    }

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

        /*        //Œü‚«‚ÌØ‚è‘Ö‚¦
                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal"));
                //ˆÚ“®
                cc.SimpleMove(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 36.0f);*/
    }
}
