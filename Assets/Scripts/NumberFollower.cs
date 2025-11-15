using UnityEngine;
using TMPro;

public class NumberFollower : MonoBehaviour
{
    public TMP_Text valueText;
    public float followLerp = 10f;

    private int orderIndex = 0;
    private int displayValue = 1;
    private int delaySamples = 10;

    public void Initialize(int order, int valueToDisplay)
    {
        orderIndex = order;
        displayValue = valueToDisplay;
        if (valueText != null) valueText.text = displayValue.ToString();
    }

    public void SetDelaySamples(int samples)
    {
        delaySamples = Mathf.Max(1, samples);
    }

    void LateUpdate()
    {
        if (PathRecorder.Instance == null) return;

        int sampleIndex = delaySamples;
        Vector3 target = PathRecorder.Instance.GetPositionAtIndex(sampleIndex);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * followLerp);

        Vector3 next = PathRecorder.Instance.GetPositionAtIndex(Mathf.Max(0, sampleIndex - 2));
        Vector3 forward = (next - target).normalized;
        if (forward.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);
        }
    }
}
