using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;
    public Node[,] nodes;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        nodes = new Node[BoxManager.instance.col, BoxManager.instance.row];
        CreateGridNode();
    }


    private void CreateGridNode()
    {
        for (int i = 0; i < BoxManager.instance.row; i++)
        {
            for (int j = 0; j < BoxManager.instance.col; j++)
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
