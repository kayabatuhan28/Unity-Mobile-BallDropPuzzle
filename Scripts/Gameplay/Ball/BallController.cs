using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float DistanceToJoint;

    public HingeJoint2D AttachBallToLastJoint(Rigidbody2D lastJoint)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = lastJoint;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0f, -DistanceToJoint);

        return joint;
    }
}
