using System.Collections.Generic;
using UnityEngine;

public class NumberChainManager : MonoBehaviour
{
    public static NumberChainManager Instance { get; private set; }

    public GameObject followerPrefab; 
    public Transform followParent;
    public float sampleInterval = 0.02f;
    public float spacingSeconds = 0.35f;

    private List<GameObject> followers = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RebuildChain(int headValue)
    {
        // remove existing followers
        foreach (var f in followers) Destroy(f);
        followers.Clear();

        // number of followers should be headValue - 1 (exclude head)
        int count = Mathf.Max(0, headValue - 1);

        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(followerPrefab, followParent);
            var nf = g.GetComponent<NumberFollower>();
            if (nf == null)
            {
                Debug.LogError("Follower prefab missing NumberFollower component!");
                continue;
            }

            // i is zero-based: first follower should display headValue - 1
            int displayValue = headValue - (i + 1);
            nf.Initialize(i + 1, displayValue);

            followers.Add(g);
        }

        // compute delay samples for each follower (first follower is closest)
        int baseSamples = Mathf.RoundToInt((spacingSeconds / sampleInterval));
        for (int i = 0; i < followers.Count; i++)
        {
            var nf = followers[i].GetComponent<NumberFollower>();
            nf.SetDelaySamples(baseSamples * (i + 1));
        }
    }
}
