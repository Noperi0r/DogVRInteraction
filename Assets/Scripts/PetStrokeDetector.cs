using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;
using Oculus.Interaction.Samples.PalmMenu;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class PetStrokeDetector : MonoBehaviour
{
    [SerializeField] HandRef hand;
    [SerializeField] GameObject rHandVisualObj;
    [SerializeField] float minSpeed = 0.5f;   // m/s
    [SerializeField] float maxVertDisp = 0.07f;// Y축 편차 허용

    Vector3 lastPos;
    bool stroking;

    void OnStrokeStart()
    {
        Debug.Log("Stroke started");
    }

    void OnStrokeUpdate(float speed)
    {
        Debug.Log($"Stroking... Speed: {speed:F2}");
    }

    void OnStrokeEnd()
    {
        Debug.Log("Stroke ended");
    }


    void Update()
    {
        if (hand == null || hand.Hand == null || !hand.Hand.IsTrackedDataValid)
        {
            stroking = false;
            return;
        }

        // palamdown track
        hand.Hand.GetJointPose(HandJointId.HandPalm, out Pose pose);
        Vector3 palmNormal = pose.rotation * Vector3.down;
        bool isPalmDown = Vector3.Dot(palmNormal, Vector3.down) > 0.5f;

        Vector3 cur = rHandVisualObj.transform.position;
        Vector3 delta = cur - lastPos;

        float speed = delta.magnitude / Time.deltaTime; // dist / t 

        // 주행 방향이 거의 수평이고 속도가 일정 이상이면 stroke
        bool horizontal = Mathf.Abs(delta.y) < maxVertDisp;

        if (speed > minSpeed && horizontal)
        {
            if (!stroking) OnStrokeStart();
            OnStrokeUpdate(speed);
            stroking = true;
        }
        else if (stroking)
        {
            OnStrokeEnd();
            stroking = false;
        }

        lastPos = cur;
    }



}
