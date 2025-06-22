using UnityEngine;

public class ColorDebug : MonoBehaviour
{
    [Header("Target Cube Renderer")]
    [SerializeField] private Renderer cubeRenderer;

    [Header("Colors")]
    [SerializeField] private Color offColor = Color.white;
    [SerializeField] private Color onColor = Color.red;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) DebugColorOn();
        else if (Input.GetKeyDown(KeyCode.F2)) DebugColorOff();

    }

    // 이벤트 래퍼에서 On 호출 시
    public void DebugColorOn()
    {
        if (cubeRenderer == null) return;
        cubeRenderer.material.color = onColor;
        Debug.Log($"[ColorDebug] OnWrapper → 색상 변경: {onColor}");
    }

    // 이벤트 래퍼에서 Off 호출 시
    public void DebugColorOff()
    {
        if (cubeRenderer == null) return;
        cubeRenderer.material.color = offColor;
        Debug.Log($"[ColorDebug] OffWrapper → 색상 복원: {offColor}");
    }
}