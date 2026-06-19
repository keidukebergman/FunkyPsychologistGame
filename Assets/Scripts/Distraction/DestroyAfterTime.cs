using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float LifeDuration = 1.0f;

    private float LifeTime;

    private void Awake()
    {
        LifeTime = LifeDuration + Time.time;
    }

    private void Update()
    {
        if (Time.time > LifeTime)
        {
            OnLifeEnd();
        }
    }

    private void OnLifeEnd()
    {
        Destroy(gameObject);
    }
}
