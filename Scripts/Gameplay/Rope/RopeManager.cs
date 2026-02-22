using UnityEngine;

public class RopeManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D FirstHook;
    [SerializeField] private BallController AttachedBall;
    [SerializeField] private int JointCount = 5;

    [SerializeField] private GameObject ropeCutVfx;

    public GameObject[] JointPool;

    private HingeJoint2D ballHinge;

    void Start()
    {
        CreateJoint();
    }

    void CreateJoint()
    {
        Rigidbody2D beforeJoint = FirstHook;

        for (int i = 0; i < JointCount; i++)
        {
            JointPool[i].SetActive(true);

            HingeJoint2D hingeJoint = JointPool[i].GetComponent<HingeJoint2D>();
            hingeJoint.connectedBody = beforeJoint;

            if (i < JointCount - 1)
            {
                beforeJoint = JointPool[i].GetComponent<Rigidbody2D>();
            }
            else
            {
                ballHinge = AttachedBall.AttachBallToLastJoint(JointPool[i].GetComponent<Rigidbody2D>());
            }
        }
    }

    public void CutFromJoint(GameObject clickedJoint)
    {
        int index = System.Array.IndexOf(JointPool, clickedJoint);
        if (index == -1) return;

        Instantiate(ropeCutVfx, clickedJoint.transform.position, Quaternion.identity);

        // Disable the clicked rope segment and lower segments
        for (int i = index; i < JointCount; i++)
        {
            JointPool[i].SetActive(false);
        }

        // Break the rope connection to the ball
        if (ballHinge != null)
        {
            ballHinge.enabled = false;
            ballHinge.connectedBody = null; 
        }
    }
}
