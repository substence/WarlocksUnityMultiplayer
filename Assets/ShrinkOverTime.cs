using UnityEngine;

public class ShrinkOverTime : MonoBehaviour
{
    [SerializeField]
    private float shrinkRate = -0.0001f;

    private void FixedUpdate()
    {
        this.transform.localScale = new Vector3(Mathf.Max(transform.localScale.x + shrinkRate, 0),
    transform.localScale.y,
    Mathf.Max(transform.localScale.z + shrinkRate, 0));
    }
}
