using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    [SerializeField] float timeMinus;
    LineRenderer line;
    Vector3 prevPos;
    [SerializeField] float minDistance;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        prevPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.x += 25;

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("LimitZone"))
                {
                    if (Vector2.Distance(mouseWorldPosition, prevPos) > minDistance)
                    {
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, mouseWorldPosition);
                        prevPos = mouseWorldPosition;
                        GameManager.instance.SusbtractTime(timeMinus * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        line.positionCount = 0;
    }

    private void OnDisable()
    {
    }
}
