using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    Line activeLine;
    public GameObject drawGameParent;

    public GameObject instruction;
    public GameObject resetButton;

    private bool gameStarted = false;

    private void Start()
    {
        instruction.SetActive(false);
        resetButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newLine = Instantiate(linePrefab, drawGameParent.transform);
                activeLine = newLine.GetComponent<Line>();
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if (activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        instruction.SetActive(true);
        resetButton.SetActive(true);
    }

    public void EndGame()
    {
        gameStarted = false;
    }

    public void ResetCanvas()
    {
        var lines = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Line(Clone)");
        foreach (var line in lines)
        {
            Destroy(line);
        }
    }
}
