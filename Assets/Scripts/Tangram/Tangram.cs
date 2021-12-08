using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangram : MonoBehaviour
{
    public bool isCorrectTransform;
    public List<Tangram> examplePieceList = new List<Tangram>();
    void Start()
    {
        examplePieceList.AddRange(FindObjectsOfType<Tangram>());
        examplePieceList.Remove(this);
    }
    private void Update()
    {
        SuccessTangram();
    }

    public void SuccessTangram()
    {
        bool isComplete = true;

        for(int i = 0; i < examplePieceList.Count; i++)
        {
            if (isComplete &= examplePieceList[i].isCorrectTransform)
                Destroy(gameObject);
        }
    }
}
