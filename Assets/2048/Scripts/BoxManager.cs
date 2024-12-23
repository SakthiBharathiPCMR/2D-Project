using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum BoxState
{
    Idle,
    Merged,
    Moved,
}


public enum InputState
{
    receive,
    block,
}

public class BoxManager : MonoBehaviour
{

    public static BoxManager instance;

    public int row;
    public int col;

    public List<NumberDataSo> numberDataSos;
    public GameObject gridObj;
    public float cellSize;
    public Vector2Int moveDir;
    public InputState inputState;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        row = NodeManager.instance.row;
        col = NodeManager.instance.col;
        CreateBoxGrid();
        SpawnBox();
        inputState = InputState.receive;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;


        if (inputDir.magnitude > 0.1f && inputState == InputState.receive)
        {
            if (Mathf.Abs(inputDir.x) > 0)
            {
                moveDir = new Vector2Int((int)inputDir.x, 0);
            }

            if (Mathf.Abs(inputDir.y) > 0)
            {
                moveDir = new Vector2Int(0, (int)inputDir.y);
            }


            MoveBox();
        }

    }

    private void MoveBox()
    {
        inputState = InputState.block;
        Vector2Int startNode = Vector2Int.zero;
        startNode.x = moveDir.x == -1 ? 0 : col - 1;
        startNode.y = moveDir.y == -1 ? 0 : row - 1;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                int x = moveDir.x == -1 ? j : startNode.x - j;
                int y = moveDir.y == -1 ? i : startNode.y - i;
                // Debug.Log(new Vector2(x, y));


                if (NodeManager.instance.nodes[x, y].nodeState == NodeState.block)
                {
                    Box curBox = NodeManager.instance.nodes[x, y].curBox;
                    if (curBox.boxState == BoxState.Idle)
                    {
                        Vector2Int curInd = curBox.curNode.gridInd;
                        int counter=0;
                        do
                        {
                            curInd += moveDir;

                            counter++;
                            if(counter>50)
                            {
                                break;
                            }
                        }
                        while (curInd.x < col || curInd.x >= 0 || curInd.y < row || curInd.y >= 0 || NodeManager.instance.nodes[curInd.x, curInd.y].nodeState == NodeState.free);



                        // while (true)
                        // {
                        //     curInd += moveDir;

                        //     // Check for boundary conditions and node state
                        //     if (curInd.x < 0 || curInd.x >= col || curInd.y < 0 || curInd.y >= row ||
                        //         NodeManager.instance.nodes[curInd.x, curInd.y].nodeState != NodeState.free)
                        //     {
                        //         break;
                        //     }
                        // }



                        curBox.transform.position = GetWorldPos(curInd);
                        curBox.SetBox(NodeManager.instance.nodes[curInd.x, curInd.y]);
                        curBox.boxState = BoxState.Moved;
                    }
                }
            }
        }
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
