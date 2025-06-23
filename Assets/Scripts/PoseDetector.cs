using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;
using Oculus.Interaction.Samples.PalmMenu;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class PoseDetector : MonoBehaviour
{
    [SerializeField] OVRCameraRig vrCam;
    [SerializeField] HandRef hand;
    [SerializeField] GameObject rHandVisualObj;
    [SerializeField] float minSpeed = 0.5f;   // m/s
    [SerializeField] float maxVertDisp = 0.02f;// Y축 편차 허용

    [Space, SerializeField] CharacterAnimController animController;

    Vector3 lastPos;
    bool ishandOpen;

    /// <summary>
    /// Applied by active state. So is Off function.
    /// </summary>
    public void HandOpenOn()
    {
        ishandOpen = true;
    }

    public void HandOpenOff()
    {
        ishandOpen = false;
    }

    void Update()
    {
        // Walk 
        //Debug.Log(vrCam.centerEyeAnchor.localPosition.y);
        // if (vrCam.centerEyeAnchor.localPosition.y <= 1.3f)
        //     animController.EnableWalk();
        // else
        //     animController.DisableWalk();

        // Hand tracking preemptively handling exceptions.
        if (hand == null || hand.Hand == null || !hand.Hand.IsTrackedDataValid) return;
    
        // palamdown track
        hand.Hand.GetJointPose(HandJointId.HandPalm, out Pose pose);
        Vector3 palmNormal = pose.rotation * Vector3.down;
        bool isPalmDown = Vector3.Dot(palmNormal, Vector3.down) > 0.5f;
        bool isPalmUp = Vector3.Dot(palmNormal, Vector3.up) > 0.5f;

        // hand speed track
        Vector3 cur = rHandVisualObj.transform.position;
        Vector3 delta = cur - lastPos;

        float speed = delta.magnitude / Time.deltaTime; // dist / t 

        // Is user shaking his hand horizontally
        bool horizontal = Mathf.Abs(delta.y) < maxVertDisp;

        // Action 1
        if (ishandOpen && speed > minSpeed && horizontal && isPalmDown)
            animController.EnableAction1();
        else
            animController.DisableAction1();

        // Action 2
        if (ishandOpen && speed <= minSpeed && isPalmUp)
            animController.EnableAction2();
        else
            animController.DisableAction2();

        lastPos = cur;
    }
}
