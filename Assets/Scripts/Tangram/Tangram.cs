using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangram : MonoBehaviour
{
    public bool isCorrectTransform;
    public List<Tangram> examplePieceList;
    public MoveOriginPos[] pieceList;
    public float maxAngle;
    public float minAngle;

    public void Start()
    {
        examplePieceList = new List<Tangram>();
        examplePieceList.AddRange(FindObjectsOfType<Tangram>());
        examplePieceList.Remove(this);

        pieceList = FindObjectsOfType<MoveOriginPos>();

        isCorrectTransform = false;
    }
    private void Update()
    {
        SuccessTangram();
    }

    public void SuccessTangram()
    {
        bool isComplete = true;
        for (int i = 0; i < examplePieceList.Count; i++)
        {
            isComplete &= examplePieceList[i].isCorrectTransform;

        }
        if (isComplete)
        {
            /*
            pieceList.ForEach(e =>
            {
                e.transform.position = e.okPosition;
            });
            */
            // Debug.Log("¿Ï¼º");
            for (int i = 0; i < pieceList.Length; i++)
            {
                pieceList[i].transform.position = pieceList[i].originPos;
                pieceList[i].transform.eulerAngles = pieceList[i].originRota;
            }


            Destroy(gameObject);
        }


    }
}
