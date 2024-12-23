using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int curBoxId;
    public int mergeBoxId;

    public BoxState boxState;

    public Node curNode;


    public void SetBox(Node node)
    {
        curNode=node;
        curNode.curBox=this;
    }

}
