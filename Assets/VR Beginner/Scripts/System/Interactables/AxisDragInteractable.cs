using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;




/// <summary>
/// Custom interactable that can be dragged along an axis. Can either be continuous or snap to integer steps.
/// </summary>
public class AxisDragInteractable : XRBaseInteractable
{
    [Serializable]
    public class DragDistanceEvent : UnityEvent<float> { }

    [Serializable]
    public class DragStepEvent : UnityEvent<int> { }

    [Tooltip("The Rigidbody that will be moved. If null will try to grab one on that object or its children")]
    public Rigidbody MovingRigidbody;



    public Vector3 LocalAxis;
    public float AxisLength;

    [Tooltip("If 0, then this is a float [0,1] range slider, otherwise there is an integer slider")]
    public int Steps = 0;
    public bool SnapOnlyOnRelease = true;

    public bool ReturnOnFree;
    public float ReturnSpeed;

    // public AudioClip SnapAudioClip;

    public DragDistanceEvent OnDragDistance;
    public DragStepEvent OnDragStep;

    Vector3 m_EndPoint;
    Vector3 m_StartPoint;
    Vector3 m_GrabbedOffset;
    float m_CurrentDistance;
    XRBaseInteractor m_GrabbingInteractor;

    float m_StepLength;


    // Start is called before the first frame update
    void Start()
    {
        LocalAxis.Normalize();

        //Length can't be negative, a negative length just mean an inverted axis, so fix that
        if (AxisLength < 0)
        {
            LocalAxis *= -1;
            AxisLength *= -1;
        }

        if (Steps == 0)
        {
            m_StepLength = 0.0f;
        }
        else
        {
            m_StepLength = AxisLength / Steps;
        }

        m_StartPoint = transform.position;
        m_EndPoint = transform.position + transform.TransformDirection(LocalAxis) * AxisLength;

        if (MovingRigidbody == null)
        {
            MovingRigidbody = GetComponentInChildren<Rigidbody>();
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isSelected)
        {
            for(int i=0; i<transform.childCount; i++)
            {
                var child = transform.GetChild(0);
                if(child.name.Equals("SelectedMesh")) 
                {
                    if(TryGetComponent<Renderer>(out Renderer renderer))
                    {
                        renderer.enabled = true;
                    }
                }
            }
            // transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
            {
                Vector3 WorldAxis = transform.TransformDirection(LocalAxis);

                Vector3 distance = m_GrabbingInteractor.transform.position - transform.position - m_GrabbedOffset;
                float projected = Vector3.Dot(distance, WorldAxis);

                Vector3 targetPoint;

                // Debug.Log(projected * moveSpeed * Time.deltaTime);

                if (projected > 0)
                    targetPoint = Vector3.MoveTowards(transform.position, m_EndPoint, projected);
                else
                    targetPoint = Vector3.MoveTowards(transform.position, m_StartPoint, -projected);

                Vector3 move = targetPoint - transform.position;

                if (MovingRigidbody != null)
                    MovingRigidbody.MovePosition(MovingRigidbody.position + move);
                else
                    transform.position = transform.position + move;
            }

            // if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Fixed)
            // {
            //     Vector3 WorldAxis = transform.TransformDirection(LocalAxis);

            //     Vector3 distance = m_GrabbingInteractor.transform.position - m_GrabbedOffset;

            //     var c = Vector3.Cross(m_GrabbedOffset, m_GrabbingInteractor.transform.position);
            //     var d = Vector3.Dot(Vector3.up, c);
            //     float moveSpeed = 2f;

            //     Vector3 moveVector;
            //     if (d < 0f)
            //         moveVector = Vector3.MoveTowards(transform.position, m_EndPoint, Vector3.Dot(distance, WorldAxis) * moveSpeed * Time.deltaTime);
            //     else
            //         moveVector = Vector3.MoveTowards(transform.position, m_StartPoint, -Vector3.Dot(distance, WorldAxis) * moveSpeed * Time.deltaTime);

            //     transform.position += (moveVector - transform.position);
            // }
        }
        else
        {
            // transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            for(int i=0; i<transform.childCount; i++)
            {
                var child = transform.GetChild(0);
                if(child.name.Equals("SelectedMesh")) 
                {
                    if(TryGetComponent<Renderer>(out Renderer renderer))
                    {
                        renderer.enabled = false;
                    }
                }
            }
            // if (ReturnOnFree)
            // {
            //     Vector3 targetPoint = Vector3.MoveTowards(transform.position, m_StartPoint, ReturnSpeed * Time.deltaTime);
            //     Vector3 move = targetPoint - transform.position;

            //     if (MovingRigidbody != null)
            //         MovingRigidbody.MovePosition(MovingRigidbody.position + move);
            //     else
            //         transform.position = transform.position + move;
            // }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        var interactor = args.interactor;
        m_GrabbedOffset = interactor.transform.position - transform.position;
        m_GrabbingInteractor = interactor;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // if (SnapOnlyOnRelease && Steps != 0)
        // {
        //     float dist = (transform.position - m_StartPoint).magnitude;
        //     int step = Mathf.RoundToInt(dist / m_StepLength);
        //     dist = step * m_StepLength;

        //     transform.position = m_StartPoint + transform.TransformDirection(LocalAxis) * dist;

        //     if (step != m_CurrentStep)
        //     {
        //         SFXPlayer.Instance.PlaySFX(SnapAudioClip, transform.position, new SFXPlayer.PlayParameters()
        //         {
        //             Pitch = Random.Range(0.9f, 1.1f),
        //             SourceID = -1,
        //             Volume = 1.0f
        //         }, 0.0f);
        //         OnDragStep.Invoke(step);
        //     }
        // }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 end = transform.position + transform.TransformDirection(LocalAxis.normalized) * AxisLength;

        Gizmos.DrawLine(transform.position, end);
        Gizmos.DrawSphere(end, 0.01f);
    }
}
