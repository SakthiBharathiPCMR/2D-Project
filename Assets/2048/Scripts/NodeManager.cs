using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;

    public int row;
    public int col;
    public Node[,] nodes;
    private void Awake()
    {
        instance = this;
        nodes = new Node[col,row];
        CreateGridNode();
    }



    private void CreateGridNode()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Node curNode = new Node();
                curNode.gridInd = new Vector2Int(j, i);
                nodes[j, i] = curNode;
            }
        }
    }


}

[System.Serializable]
public class Node
{
    public Vector2Int gridInd;
    public Box curBox;
    public NodeState nodeState;
}

public enum NodeState
{
    free,
    block,

}
