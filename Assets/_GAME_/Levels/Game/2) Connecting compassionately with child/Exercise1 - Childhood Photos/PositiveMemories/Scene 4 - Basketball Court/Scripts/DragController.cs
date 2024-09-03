using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rb;

    public float dragLimit = 3f;
    public float forceToAdd = 10f;
    private CinemachineVirtualCamera virtualCamera;
    private Camera cam;
    private bool isDragging;

    private bool gameEnabled;

    Vector3 MousePosition
    {
        get
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
    }

    private void Start()
    {
        gameEnabled = true;
        cam = Camera.main;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (gameEnabled)
        {
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                DragStart();
            }

            if (isDragging)
            {
                Drag();
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                DragEnd();
            }
        }
    }

    private void DragStart()
    {
        lineRenderer.enabled = true;
        isDragging = true;
        lineRenderer.SetPosition(0, MousePosition);
    }

    private void Drag()
    {
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 currentPos = MousePosition;
        Vector3 distance = currentPos - startPos;

        if (distance.magnitude <= dragLimit)
        {
            lineRenderer.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 limitVector = startPos + (distance.normalized * dragLimit);
            lineRenderer.SetPosition(1, limitVector);
        }
    }

    private void DragEnd()
    {
        isDragging = false;
        lineRenderer.enabled = false;

        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 currentPos = lineRenderer.GetPosition(1);
        Vector3 distance = currentPos - startPos;

        Vector3 finalForce = distance * forceToAdd;

        rb.AddForce(-finalForce, ForceMode2D.Impulse);

    }

    public void EnableGame()
    {
        gameEnabled = true;
    }

    public void DisableGame()
    {
        DragEnd();
        gameEnabled = false;
    }

}
