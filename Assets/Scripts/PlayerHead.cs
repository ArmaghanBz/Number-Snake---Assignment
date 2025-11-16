using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerHead : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 6f;
    public float horizontalSpeed = 12f;
    public float laneWidth = 3f;

    [Header("Swipe / Input")]
    public float swipeSensitivity = 0.01f;

    [Header("Number")]
    public int headValue = 1;
    public TMP_Text valueText;

    [Header("References")]
    public GameObject followerPrefab;
    public float followerSpacing = 0.35f;

    private Rigidbody rb;
    private Vector2 lastPointerPos;
    private bool touching = false;

    // smooth motion
    private float targetX;
    private float velocityX; 
    public float smoothTime = 0.08f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetX = transform.position.x;
        UpdateValueText();

        if (PathRecorder.Instance == null)
        {
            GameObject go = new GameObject("PathRecorder");
            go.AddComponent<PathRecorder>();
        }
    }

    void Update()
    {
        HandleInput();
        
    }

    void FixedUpdate()
    {
        Vector3 vel = transform.forward * forwardSpeed;
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        SmoothHorizontalMovement();
        if (PathRecorder.Instance != null) PathRecorder.Instance.Record(transform.position);
    }

    void HandleInput()
    {
        // Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            touching = true;
            lastPointerPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
            touching = false;

        if (touching)
        {
            Vector2 cur = Input.mousePosition;
            Vector2 delta = cur - lastPointerPos;
            lastPointerPos = cur;

            float dx = delta.x * swipeSensitivity * horizontalSpeed * Time.deltaTime;
            targetX += dx;
        }

        // Keyboard Input
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(h) > 0.01f)
        {
            targetX += h * horizontalSpeed * Time.deltaTime;
        }

        targetX = Mathf.Clamp(targetX, -laneWidth, laneWidth);
    }

    void SmoothHorizontalMovement()
    {
        float newX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocityX, smoothTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            var pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                CollectPickup(pickup.value);
                pickup.Collect();
            }
        }
        else if (other.CompareTag("Obstacle"))
        {
            var obs = other.GetComponent<Obstacle>();
            if (obs != null) obs.Hit(this);
        }
        else if (other.CompareTag("end"))
        {
            var fin = other.GetComponent<FinishLine>();
            if (fin != null) fin.ReachFinish();
        }
    }

    public void CollectPickup(int value)
    {
        headValue += value;
        UpdateValueText();
        NumberChainManager.Instance.RebuildChain(headValue);

        StartCoroutine(PopEffect());
      
    }

    public void RemoveTailCount(int amount)
    {
        headValue = Mathf.Max(1, headValue - amount);
        UpdateValueText();
        NumberChainManager.Instance.RebuildChain(headValue);
    }

    private IEnumerator PopEffect()
    {
        Vector3 orig = transform.localScale;
        float t = 0f;
        while (t < 0.12f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(orig, orig * 1.18f, t / 0.12f);
            yield return null;
        }
        transform.localScale = orig;
    }

    void UpdateValueText()
    {
        if (valueText != null) valueText.text = headValue.ToString();
    }
}
