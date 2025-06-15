using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;
using UnityEngine;

public class PetStrokeDetector : MonoBehaviour
{
    [SerializeField] HandRef hand;
    [SerializeField] float minSpeed = 0.10f;   // m/s
    [SerializeField] float maxVertDisp = 0.05f;// Y축 편차 허용

    int strokedCount;
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

        Vector3 cur = hand.transform.position;
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
            strokedCount++;
            OnStrokeEnd();
            stroking = false;
        }

        lastPos = cur;
    }



}
