using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoxState
{
    Idle,
    Merged,
    Moved,
}

public class BoxManager : MonoBehaviour
{

    public static BoxManager instance;

    public int row;
    public int col;

    public List<NumberDataSo> numberDataSos;
    public GameObject gridObj;
    public float cellSize;
    public Vector2 moveDir;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreateBoxGrid();
        SpawnBox();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (inputDir.magnitude > 0.1f)
        {
            if (Mathf.Abs(inputDir.x) > 0)
            {
                moveDir = new Vector2(inputDir.x, 0);
            }

            if (Mathf.Abs(inputDir.y) > 0)
            {
                moveDir = new Vector2(0, inputDir.y);
            }


            MoveBox();
        }

    }

    private void MoveBox()
    {

    }


    private void CreateBoxGrid()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                GameObject curGrid = Instantiate(gridObj, transform);
                curGrid.name = j + " " + i;
                curGrid.transform.position = GetWorldPos(new Vector2Int(j, i));
            }
        }
    }

    private Vector3 GetWorldPos(Vector2Int vector2Int)
    {

        Vector3 worlPos = (new Vector3(vector2Int.x, vector2Int.y, 0) - (new Vector3(col - 1, row - 1) / 2)) * cellSize;
        worlPos.z = -0.5f;
        return worlPos;
    }


    private Box GetBox(int id)
    {
        foreach (NumberDataSo data in numberDataSos)
        {
            if (data.id == id)
            {
                return data.boxObj;
            }
        }
        return null;
    }


    private void SpawnBox()
    {
        int randX;
        int randY;
        do
        {

            randX = Random.Range(0, col);
            randY = Random.Range(0, row);

        }
        while (NodeManager.instance.nodes[randX, randY].nodeState == NodeState.block);

        Box curBox = Instantiate(GetBox(1), transform);
        curBox.SetBox(NodeManager.instance.nodes[randX, randY]);
        curBox.boxState = BoxState.Idle;
        curBox.transform.position = GetWorldPos(new Vector2Int(randX, randY));



    }
}
