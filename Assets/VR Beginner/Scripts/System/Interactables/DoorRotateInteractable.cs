using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;




/// <summary>
/// Custom interactable that can be dragged along an axis. Can either be continuous or snap to integer steps.
/// </summary>
public class DoorRotateInteractable : XRBaseInteractable
{
    public enum OPENDIR
    {
        RIGHT,
        LEFT,
        // UP,
        // DOWN
    }

    public OPENDIR openDir;

    [Tooltip("If 0, then this is a float [0,1] range slider, otherwise there is an integer slider")]
    public int Steps = 0;

    Vector3 m_EndPoint;
    Vector3 m_StartPoint;
    Vector3 m_GrabbedOffset;
    float m_CurrentDistance;
    int m_CurrentStep;
    XRBaseInteractor m_GrabbingInteractor;

    float m_StepLength;

    // 문이 열리는 각도를 계산하기위한 기준점
    Vector3 anglePosition;
    float prevAngle = 0;
    float curAngle;
    int toggleAngle;


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isSelected)
        {
            transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
            {
                // 그립 버튼으로 잡았을 때 물체와의 벡터
                Vector3 transStandardDirection = m_GrabbedOffset - anglePosition;
                Vector3 transCurDirection = m_GrabbingInteractor.transform.position - anglePosition;

                Vector3 a = Vector3.Scale(transStandardDirection, new Vector3(1f, 0f, 1f));
                Vector3 b = Vector3.Scale(transCurDirection, new Vector3(1f, 0f, 1f));

                curAngle = Vector3.Angle(b, a);

                float realAngle = 0;

                realAngle = curAngle - prevAngle;

                transform.Rotate(transform.up, realAngle * toggleAngle);

                prevAngle = curAngle;
            }
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        }
    }



    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        var interactor = args.interactor;
        m_GrabbedOffset = interactor.transform.position;
        m_GrabbingInteractor = interactor;

        prevAngle = 0;

        if ((openDir) == OPENDIR.RIGHT)
        {
            anglePosition = interactor.transform.position + transform.right * 1f;
            toggleAngle = -1;
        }
        else if ((openDir) == OPENDIR.LEFT)
        {
            anglePosition = interactor.transform.position + transform.right * -1f;
            toggleAngle = 1;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
    }
}
