using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Trying to use this as a way to center the player at
/// the start of game
/// </summary>
public class HeadCollider : MonoBehaviour
{
    public Camera camera; //this is supposed to be the camera for eyes
    //CapsuleCollider collider;
    CharacterController collider;

    public Transform VRCamHolder; //parent of the cam
    public float WaitBeforeCompensation = 0.5f;
    private Vector3 deltaPos;
    private Vector3 temp;

    [System.Obsolete]
    private void Start()
    {
        //collider = GetComponent<CapsuleCollider>();
        collider = GetComponent<CharacterController>();

        StartCoroutine(Centering());
    }

    private void Update()
    {
        /*So this will take the position from the center of the camera (aka the eyes) and
         * subtract the position on the playercontroller
         * which means that when we move around it gets the position
         * of the camera relative to the playercontroller*/
        Vector3 temp = camera.transform.position - this.transform.position;
        temp.y = collider.transform.position.y;
        collider.center = temp;
    }

    [System.Obsolete]
    IEnumerator Centering()
    {
        yield return new WaitForSeconds(WaitBeforeCompensation);
        deltaPos = InputTracking.GetLocalPosition(XRNode.Head);
        temp = Vector3.zero;
        temp.x = deltaPos.x;
        temp.y = deltaPos.y; //this might need to be tweaked
        temp.z = deltaPos.z;
        VRCamHolder.position += temp;
    }
}
