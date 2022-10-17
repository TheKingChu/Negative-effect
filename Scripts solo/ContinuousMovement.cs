using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class ContinuousMovement : MonoBehaviour
{
    private XROrigin rig;

    private Vector2 inputAxis;

    private CharacterController character;

    //gravity stuff
    public float gravity = -9.81f; //the gravity of earth (funfact)
    private float fallingSpeed;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;

        Quaternion headY = Quaternion.Euler(0, rig.CameraFloorOffsetObject.transform.eulerAngles.y, 0);

        //gravity stuff
        bool isGrounded = CheckIfGrounded();
        if (isGrounded) //makes the falling more smooth (hopefully)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        /*testing if this will make the character run
        if(controller.TryGetFeatureValue(primary2DVector, out primary2dValue) && primary2dValue != Vector2.zero)
        {

        }*/
    }

    //this makes sure that the player wont like bump into stuff irl
    private void CapsuleFollowHeadset()
    {
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.gameObject.transform.position); //places the capsule where your head is
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    private bool CheckIfGrounded()
    {
        //checks if the player is on the ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f; //makes the ray a bit bigger than the character capsule
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
