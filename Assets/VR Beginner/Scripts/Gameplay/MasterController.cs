using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

public class MasterController : MonoBehaviour
{
    static MasterController s_Instance = null;
    public static MasterController Instance => s_Instance;

    public XRRig Rig => m_Rig;

    [Header("Setup")]
    public bool DisableSetupForDebug = false;

    [Header("Reference")]
    public XRRayInteractor RightInteractor;
    public XRRayInteractor LeftInteractor;

    XRRig m_Rig;

    InputDevice m_LeftInputDevice;
    InputDevice m_RightInputDevice;

    XRInteractorLineVisual m_RightLineVisual;
    XRInteractorLineVisual m_LeftLineVisual;

    HandPrefab m_RightHandPrefab;
    HandPrefab m_LeftHandPrefab;

    XRReleaseController m_RightController;
    XRReleaseController m_LeftController;

    bool m_LastFrameRightEnable = false;

    LayerMask m_OriginalRightMask;
    LayerMask m_OriginalLeftMask;

    LineRenderer m_LeftLineRenderer;
    LineRenderer m_RightLineRenderer;

    List<XRBaseInteractable> m_InteractableCache = new List<XRBaseInteractable>(16);

    void Awake()
    {
        s_Instance = this;
        m_Rig = GetComponent<XRRig>();

    }

    void OnEnable()
    {
        InputDevices.deviceConnected += RegisterDevices;
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= RegisterDevices;
    }

    void Start()
    {
        m_RightLineVisual = RightInteractor.GetComponent<XRInteractorLineVisual>();
        m_RightLineVisual.enabled = false;

        m_LeftLineVisual = LeftInteractor.GetComponent<XRInteractorLineVisual>();
        m_LeftLineVisual.enabled = false;

        m_RightController = RightInteractor.GetComponent<XRReleaseController>();
        m_LeftController = LeftInteractor.GetComponent<XRReleaseController>();

        m_OriginalRightMask = RightInteractor.interactionLayerMask;
        m_OriginalLeftMask = LeftInteractor.interactionLayerMask;

        m_RightLineRenderer = RightInteractor.GetComponent<LineRenderer>();
        m_LeftLineRenderer = LeftInteractor.GetComponent<LineRenderer>();

        InputDeviceCharacteristics leftTrackedControllerFilter = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left;
        List<InputDevice> foundControllers = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(leftTrackedControllerFilter, foundControllers);

        if (foundControllers.Count > 0)
            m_LeftInputDevice = foundControllers[0];


        InputDeviceCharacteristics rightTrackedControllerFilter = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right;
        InputDevices.GetDevicesWithCharacteristics(rightTrackedControllerFilter, foundControllers);

        if (foundControllers.Count > 0)
            m_RightInputDevice = foundControllers[0];

    }

    void RegisterDevices(InputDevice connectedDevice)
    {
        if (connectedDevice.isValid)
        {
            if ((connectedDevice.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
            {
                if ((connectedDevice.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
                {
                    m_LeftInputDevice = connectedDevice;
                }
                else if ((connectedDevice.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
                {
                    m_RightInputDevice = connectedDevice;
                }
            }
        }
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            Application.Quit();

        RightControllertUpdate();
    }

    void RightControllertUpdate()
    {
        bool isTriggerButton;
        bool isGripButton;
        m_RightInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerButton);
        m_RightInputDevice.TryGetFeatureValue(CommonUsages.gripButton, out isGripButton);

        m_RightLineVisual.enabled = isTriggerButton;
        // m_RightLineVisual.enabled = !isGripButton && isTriggerButton;

        if (isTriggerButton && isGripButton)
        {
            m_RightController.Select();
            m_RightLineVisual.lineLength = 0.1f;
        }
        else
        {
            m_RightController.UnSelect();
            m_RightLineVisual.lineLength = 20f;
        }

        RightInteractor.interactionLayerMask = m_LastFrameRightEnable ? m_OriginalRightMask : new LayerMask();

        m_LastFrameRightEnable = m_RightLineVisual.enabled;
    }
}
