using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public event Action<IHitable> Hitted;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Hit();
        }
    }

    private void Hit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.transform != null && hit.transform.TryGetComponent(out IHitable hitable))
        {
            Hitted?.Invoke(hitable);
        }
    }
}