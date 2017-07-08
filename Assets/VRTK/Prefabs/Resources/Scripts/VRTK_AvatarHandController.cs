// Avatar Hand Controller|Prefabs|0100
namespace VRTK
{
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    [Serializable]
    public sealed class AxisOverrides
    {
        [Header("Global Override Settings")]

        [Tooltip("Determines whether to ignore all of the given overrides on an Interaction event.")]
        public bool ignoreAllOverrides = true;

        [Header("Thumb Override Settings")]

        [Tooltip("Determines whether to apply the given thumb override.")]
        public bool applyThumbOverride = true;
        [Tooltip("The axis override for the thumb on an Interact Touch event. Will only be applicable if the thumb button state is not touched.")]
        [Range(0f, 1f)]
        public float thumbOverride;

        [Header("Index Finger Override Settings")]

        [Tooltip("Determines whether to apply the given index finger override.")]
        public bool applyIndexOverride = true;
        [Tooltip("The axis override for the index finger on an Interact Touch event. Will only be applicable if the index finger button state is not touched.")]
        [Range(0f, 1f)]
        public float indexOverride;

        [Header("Middle Finger Override Settings")]

        [Tooltip("Determines whether to apply the given middle finger override.")]
        public bool applyMiddleOverride = true;
        [Tooltip("The axis override for the middle finger on an Interact Touch event. Will only be applicable if the middle finger button state is not touched.")]
        [Range(0f, 1f)]
        public float middleOverride;

        [Header("Ring Finger Override Settings")]

        [Tooltip("Determines whether to apply the given ring finger override.")]
        public bool applyRingOverride = true;
        [Tooltip("The axis override for the ring finger on an Interact Touch event. Will only be applicable if the ring finger button state is not touched.")]
        [Range(0f, 1f)]
        public float ringOverride;

        [Header("Pinky Finger Override Settings")]

        [Tooltip("Determines whether to apply the given pinky finger override.")]
        public bool applyPinkyOverride = true;
        [Tooltip("The axis override for the pinky finger on an Interact Touch event.  Will only be applicable if the pinky finger button state is not touched.")]
        [Range(0f, 1f)]
        public float pinkyOverride;
    }

    /// <summary>
    /// Provides a custom controller hand model with psuedo finger functionality.
    /// </summary>
    /// <remarks>
    /// **Prefab Usage:**
    ///  * Place the `VRTK/Prefabs/AvatarHands/VRTK_BasicHand` prefab as a child of either the left or right script alias.
    ///  * If the prefab is being used in the left hand then check the `Mirror Model` parameter.
    ///  * By default, the avatar hand controller will detect which controller is connected and represent it accordingly.
    ///  * Optionally, use SDKTransformModify scripts to adjust the hand orientation based on different controller types.
    /// </remarks>
    /// <example>
    /// `032_Controller_CustomControllerModel` uses the `VRTK_BasicHand` prefab to display custom avatar hands for the left and right controller.
    /// </example>
    public class VRTK_AvatarHandController : MonoBehaviour
    {
        [Header("Hand Settings")]

        [Tooltip("Determines whether the Finger and State settings are auto set based on the connected controller type.")]
        public bool autoDetectController = true;
        [Tooltip("If this is checked then the model will be mirrored, tick this if the avatar hand is for the left hand controller.")]
        public bool mirrorModel = false;
        [Tooltip("The speed in which a finger will transition to it's destination position if the finger state is `Digital`.")]
        public float animationSnapSpeed = 0.1f;

        [Header("Digital Finger Settings")]

        [Tooltip("The button alias to control the thumb if the thumb state is `Digital`.")]
        public VRTK_ControllerEvents.ButtonAlias thumbButton = VRTK_ControllerEvents.ButtonAlias.TouchpadTouch;
        [Tooltip("The button alias to control the index finger if the index finger state is `Digital`.")]
        public VRTK_ControllerEvents.ButtonAlias indexButton = VRTK_ControllerEvents.ButtonAlias.TriggerPress;
        [Tooltip("The button alias to control the middle finger if the middle finger state is `Digital`.")]
        public VRTK_ControllerEvents.ButtonAlias middleButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
        [Tooltip("The button alias to control the ring finger if the ring finger state is `Digital`.")]
        public VRTK_ControllerEvents.ButtonAlias ringButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
        [Tooltip("The button alias to control the pinky finger if the pinky finger state is `Digital`.")]
        public VRTK_ControllerEvents.ButtonAlias pinkyButton = VRTK_ControllerEvents.ButtonAlias.Undefined;
        [Tooltip("The button alias to control the middle, ring and pinky finger if the three finger state is `Digital`.")]
        public VRTK_ControllerEvents.ButtonAlias threeFingerButton = VRTK_ControllerEvents.ButtonAlias.GripPress;

        [Header("Axis Finger Settings")]

        [Tooltip("The button type to listen for axis changes to control the thumb.")]
        public SDK_BaseController.ButtonTypes thumbAxisButton = SDK_BaseController.ButtonTypes.Touchpad;
        [Tooltip("The button type to listen for axis changes to control the index finger.")]
        public SDK_BaseController.ButtonTypes indexAxisButton = SDK_BaseController.ButtonTypes.Trigger;
        [Tooltip("The button type to listen for axis changes to control the middle finger.")]
        public SDK_BaseController.ButtonTypes middleAxisButton = SDK_BaseController.ButtonTypes.MiddleFinger;
        [Tooltip("The button type to listen for axis changes to control the ring finger.")]
        public SDK_BaseController.ButtonTypes ringAxisButton = SDK_BaseController.ButtonTypes.RingFinger;
        [Tooltip("The button type to listen for axis changes to control the pinky finger.")]
        public SDK_BaseController.ButtonTypes pinkyAxisButton = SDK_BaseController.ButtonTypes.PinkyFinger;
        [Tooltip("The button type to listen for axis changes to control the middle, ring and pinky finger.")]
        public SDK_BaseController.ButtonTypes threeFingerAxisButton = SDK_BaseController.ButtonTypes.Grip;

        [Header("Finger State Settings")]

        [Tooltip("The Axis Type to utilise when dealing with the thumb state. Not all controllers support all axis types on all of the available buttons.")]
        public VRTK_ControllerEvents.AxisType thumbState = VRTK_ControllerEvents.AxisType.Digital;
        public VRTK_ControllerEvents.AxisType indexState = VRTK_ControllerEvents.AxisType.Digital;
        public VRTK_ControllerEvents.AxisType middleState = VRTK_ControllerEvents.AxisType.Digital;
        public VRTK_ControllerEvents.AxisType ringState = VRTK_ControllerEvents.AxisType.Digital;
        public VRTK_ControllerEvents.AxisType pinkyState = VRTK_ControllerEvents.AxisType.Digital;
        public VRTK_ControllerEvents.AxisType threeFingerState = VRTK_ControllerEvents.AxisType.Digital;

        [Header("Finger Axis Overrides")]

        [Tooltip("Finger axis overrides on an Interact Touch event.")]
        public AxisOverrides touchOverrides;
        [Tooltip("Finger axis overrides on an Interact Grab event.")]
        public AxisOverrides grabOverrides;
        [Tooltip("Finger axis overrides on an Interact Use event.")]
        public AxisOverrides useOverrides;

        [Header("Custom Settings")]

        [Tooltip("The controller to listen for the events on. If this is left blank as it will be auto populated by finding the Controller Events script on the parent GameObject.")]
        public VRTK_ControllerEvents controllerEvents;
        [Tooltip("An optional Interact Touch to listen for touch events on. If this is left blank as it will attempt to be auto populated by finding the Interact Touch script on the parent GameObject.")]
        public VRTK_InteractTouch interactTouch;
        [Tooltip("An optional Interact Grab to listen for grab events on. If this is left blank as it will attempt to be auto populated by finding the Interact Grab script on the parent GameObject.")]
        public VRTK_InteractGrab interactGrab;
        [Tooltip("An optional Interact Use to listen for use events on. If this is left blank as it will attempt to be auto populated by finding the Interact Use script on the parent GameObject.")]
        public VRTK_InteractUse interactUse;

        #region Protected class variables
        protected Animator animator;
        protected SDK_BaseController.ControllerType controllerType;
        protected Dictionary<VRTK_ControllerEvents.ButtonAlias, ControllerInteractionEventHandler> buttonHandlers;

        protected enum OverrideState
        {
            NoOverride,
            IsOverriding,
            WasOverring,
            KeepOverring
        }

        // Index Finger Mapping: 0 = thumb, 1 = index, 2 = middle, 3 = ring, 4 = pinky
        protected bool[] fingerStates = new bool[5];
        protected bool[] fingerChangeStates = new bool[5];
        protected float[] fingerAxis = new float[5];
        protected float[] fingerRawAxis = new float[5];
        protected float[] fingerUntouchedAxis = new float[5];
        protected float[] fingerSaveAxis = new float[5];
        protected float[] fingerForceAxis = new float[5];

        protected OverrideState[] overrideAxisValues = new OverrideState[5];
        protected Coroutine[] fingerAnimationRoutine = new Coroutine[5];

        #endregion Protected class variables

        #region MonoBehaviour methods
        protected virtual void OnEnable()
        {
            animator = GetComponent<Animator>();
            controllerEvents = (controllerEvents != null ? controllerEvents : GetComponentInParent<VRTK_ControllerEvents>());
            interactTouch = (interactTouch != null ? interactTouch : GetComponentInParent<VRTK_InteractTouch>());
            interactGrab = (interactGrab != null ? interactGrab : GetComponentInParent<VRTK_InteractGrab>());
            interactUse = (interactUse != null ? interactUse : GetComponentInParent<VRTK_InteractUse>());
            SetupHandlers();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeEvents();
            controllerType = SDK_BaseController.ControllerType.Undefined;
            for (int i = 0; i < fingerAnimationRoutine.Length; i++)
            {
                if (fingerAnimationRoutine[i] != null)
                {
                    fingerAnimationRoutine = null;
                }
            }
        }

        protected virtual void Update()
        {
            if (controllerType == SDK_BaseController.ControllerType.Undefined)
            {
                DetectController();
            }

            if (animator != null)
            {
                ProcessFinger(thumbState, 0);
                ProcessFinger(indexState, 1);
                ProcessFinger(middleState, 2);
                ProcessFinger(ringState, 3);
                ProcessFinger(pinkyState, 4);
            }
        }
        #endregion MonoBehaviour methods

        #region Subscription Managers
        protected virtual void SetupHandlers()
        {
            buttonHandlers = new Dictionary<VRTK_ControllerEvents.ButtonAlias, ControllerInteractionEventHandler>();
            buttonHandlers[thumbButton] = DoThumbEvent;
            buttonHandlers[indexButton] = DoIndexEvent;
            buttonHandlers[middleButton] = DoMiddleEvent;
            buttonHandlers[ringButton] = DoRingEvent;
            buttonHandlers[pinkyButton] = DoPinkyEvent;
            buttonHandlers[threeFingerButton] = DoThreeFingerEvent;
        }

        protected virtual void SubscribeEvents()
        {
            if (controllerEvents != null)
            {
                foreach (KeyValuePair<VRTK_ControllerEvents.ButtonAlias, ControllerInteractionEventHandler> buttonHandler in buttonHandlers)
                {
                    if (buttonHandler.Key != VRTK_ControllerEvents.ButtonAlias.Undefined)
                    {
                        controllerEvents.SubscribeToButtonAliasEvent(buttonHandler.Key, true, buttonHandler.Value);
                        controllerEvents.SubscribeToButtonAliasEvent(buttonHandler.Key, false, buttonHandler.Value);
                    }
                }
                controllerEvents.SubscribeToAxisAliasEvent(thumbAxisButton, thumbState, DoThumbAxisEvent);
                controllerEvents.SubscribeToAxisAliasEvent(indexAxisButton, indexState, DoIndexAxisEvent);
                controllerEvents.SubscribeToAxisAliasEvent(middleAxisButton, middleState, DoMiddleAxisEvent);
                controllerEvents.SubscribeToAxisAliasEvent(ringAxisButton, ringState, DoRingAxisEvent);
                controllerEvents.SubscribeToAxisAliasEvent(pinkyAxisButton, pinkyState, DoPinkyAxisEvent);
                controllerEvents.SubscribeToAxisAliasEvent(threeFingerAxisButton, threeFingerState, DoThreeFingerAxisEvent);
            }

            if (interactTouch != null)
            {
                interactTouch.ControllerTouchInteractableObject += DoControllerTouch;
                interactTouch.ControllerUntouchInteractableObject += DoControllerUntouch;
            }

            if (interactGrab != null)
            {
                interactGrab.ControllerGrabInteractableObject += DoControllerGrab;
                interactGrab.ControllerUngrabInteractableObject += DoControllerUngrab;
            }

            if (interactUse != null)
            {
                interactUse.ControllerUseInteractableObject += DoControllerUse;
                interactUse.ControllerUnuseInteractableObject += DoControllerUnuse;
            }
        }

        protected virtual void UnsubscribeEvents()
        {
            if (controllerEvents != null)
            {
                foreach (KeyValuePair<VRTK_ControllerEvents.ButtonAlias, ControllerInteractionEventHandler> buttonHandler in buttonHandlers)
                {
                    if (buttonHandler.Key != VRTK_ControllerEvents.ButtonAlias.Undefined)
                    {
                        controllerEvents.UnsubscribeToButtonAliasEvent(buttonHandler.Key, true, buttonHandler.Value);
                        controllerEvents.UnsubscribeToButtonAliasEvent(buttonHandler.Key, false, buttonHandler.Value);
                    }
                }

                controllerEvents.UnsubscribeToAxisAliasEvent(thumbAxisButton, thumbState, DoThumbAxisEvent);
                controllerEvents.UnsubscribeToAxisAliasEvent(indexAxisButton, indexState, DoIndexAxisEvent);
                controllerEvents.UnsubscribeToAxisAliasEvent(middleAxisButton, middleState, DoMiddleAxisEvent);
                controllerEvents.UnsubscribeToAxisAliasEvent(ringAxisButton, ringState, DoRingAxisEvent);
                controllerEvents.UnsubscribeToAxisAliasEvent(pinkyAxisButton, pinkyState, DoPinkyAxisEvent);
                controllerEvents.UnsubscribeToAxisAliasEvent(threeFingerAxisButton, threeFingerState, DoThreeFingerAxisEvent);
            }

            if (interactTouch != null)
            {
                interactTouch.ControllerTouchInteractableObject -= DoControllerTouch;
                interactTouch.ControllerUntouchInteractableObject -= DoControllerUntouch;
            }

            if (interactGrab != null)
            {
                interactGrab.ControllerGrabInteractableObject -= DoControllerGrab;
                interactGrab.ControllerUngrabInteractableObject -= DoControllerUngrab;
            }

            if (interactUse != null)
            {
                interactUse.ControllerUseInteractableObject -= DoControllerUse;
                interactUse.ControllerUnuseInteractableObject -= DoControllerUnuse;
            }
        }
        #endregion Subscription Managers

        #region Event methods
        protected virtual void SetFingerEvent(int fingerIndex, ControllerInteractionEventArgs e)
        {
            if (overrideAxisValues[fingerIndex] == OverrideState.NoOverride)
            {
                fingerChangeStates[fingerIndex] = true;
                fingerStates[fingerIndex] = (e.buttonPressure == 0f ? false : true);
            }
        }

        protected virtual void SetFingerAxisEvent(int fingerIndex, ControllerInteractionEventArgs e)
        {
            fingerRawAxis[fingerIndex] = e.buttonPressure;
            if (overrideAxisValues[fingerIndex] == OverrideState.NoOverride)
            {
                fingerAxis[fingerIndex] = e.buttonPressure;
            }
        }

        protected virtual void DoThumbEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerEvent(0, e);
        }

        protected virtual void DoIndexEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerEvent(1, e);
        }

        protected virtual void DoMiddleEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerEvent(2, e);
        }

        protected virtual void DoRingEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerEvent(3, e);
        }

        protected virtual void DoPinkyEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerEvent(4, e);
        }

        protected virtual void DoThreeFingerEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerEvent(2, e);
            SetFingerEvent(3, e);
            SetFingerEvent(4, e);
        }

        protected virtual void DoThumbAxisEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerAxisEvent(0, e);
        }

        protected virtual void DoIndexAxisEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerAxisEvent(1, e);
        }

        protected virtual void DoMiddleAxisEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerAxisEvent(2, e);
        }

        protected virtual void DoRingAxisEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerAxisEvent(3, e);
        }

        protected virtual void DoPinkyAxisEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerAxisEvent(4, e);
        }

        protected virtual void DoThreeFingerAxisEvent(object sender, ControllerInteractionEventArgs e)
        {
            SetFingerAxisEvent(2, e);
            SetFingerAxisEvent(3, e);
            SetFingerAxisEvent(4, e);
        }

        protected virtual bool IsButtonPressed(int arrayIndex)
        {
            return (fingerStates[arrayIndex] || fingerRawAxis[arrayIndex] > 0f);
        }

        protected virtual void SaveFingerAxis(int arrayIndex, float updateAxis)
        {
            fingerSaveAxis[arrayIndex] = (fingerSaveAxis[arrayIndex] != fingerForceAxis[arrayIndex] ? updateAxis : fingerSaveAxis[arrayIndex]);
        }

        protected virtual void HandleOverrideOn(bool ignoreAllOverrides, float[] givenFingerAxis, bool[] overridePermissions, float[] overrideValues)
        {
            if (!ignoreAllOverrides)
            {
                for (int i = 0; i < overrideAxisValues.Length; i++)
                {
                    if (overridePermissions[i] && !IsButtonPressed(i) && overrideAxisValues[i] != OverrideState.WasOverring)
                    {
                        SetOverrideValue(i, ref overrideAxisValues, OverrideState.IsOverriding);
                        if (overrideAxisValues[i] == OverrideState.NoOverride)
                        {
                            fingerUntouchedAxis[i] = givenFingerAxis[i];
                        }
                        SaveFingerAxis(i, givenFingerAxis[i]);
                        fingerForceAxis[i] = overrideValues[i];
                    }
                }
            }
        }

        protected virtual void HandleOverrideOff(bool ignoreAllOverrides, bool[] overridePermissions, bool keepOverriding)
        {
            if (!ignoreAllOverrides)
            {
                for (int i = 0; i < fingerAxis.Length; i++)
                {
                    if (overridePermissions[i] && !IsButtonPressed(i) && overrideAxisValues[i] == OverrideState.IsOverriding)
                    {
                        SetOverrideValue(i, ref overrideAxisValues, (keepOverriding ? OverrideState.KeepOverring : OverrideState.WasOverring));
                        fingerAxis[i] = fingerForceAxis[i];
                        fingerForceAxis[i] = fingerSaveAxis[i];
                    }
                }
            }
        }

        protected virtual float CorrectOverrideValue(float givenOverride)
        {
            return (givenOverride == 0f ? 0.0001f : givenOverride);
        }

        protected virtual bool[] GetOverridePermissions(AxisOverrides overrideType)
        {
            bool[] overrides = new bool[]
            {
                overrideType.applyThumbOverride,
                overrideType.applyIndexOverride,
                overrideType.applyMiddleOverride,
                overrideType.applyRingOverride,
                overrideType.applyPinkyOverride
            };
            return overrides;
        }

        protected virtual float[] GetOverrideValues(AxisOverrides overrideType)
        {
            float[] overrides = new float[]
            {
                CorrectOverrideValue(overrideType.thumbOverride),
                CorrectOverrideValue(overrideType.indexOverride),
                CorrectOverrideValue(overrideType.middleOverride),
                CorrectOverrideValue(overrideType.ringOverride),
                CorrectOverrideValue(overrideType.pinkyOverride)
            };
            return overrides;
        }

        protected virtual void DoControllerTouch(object sender, ObjectInteractEventArgs e)
        {
            HandleOverrideOn(touchOverrides.ignoreAllOverrides, fingerAxis, GetOverridePermissions(touchOverrides), GetOverrideValues(touchOverrides));
        }

        protected virtual void DoControllerUntouch(object sender, ObjectInteractEventArgs e)
        {
            for (int i = 0; i < fingerUntouchedAxis.Length; i++)
            {
                if (!IsButtonPressed(i))
                {
                    SetOverrideValue(i, ref overrideAxisValues, OverrideState.WasOverring);
                    fingerForceAxis[i] = fingerUntouchedAxis[i];
                }
            }
            HandleOverrideOff(touchOverrides.ignoreAllOverrides, GetOverridePermissions(touchOverrides), false);
        }

        protected virtual void DoControllerGrab(object sender, ObjectInteractEventArgs e)
        {
            bool isUsing = (interactUse != null && interactUse.GetUsingObject() != null);
            float[] overrideValues = (GetOverrideValues((isUsing ? useOverrides : grabOverrides)));
            float[] overrideFingerAxis = (isUsing ? GetOverrideValues(grabOverrides) : fingerAxis);
            HandleOverrideOn(grabOverrides.ignoreAllOverrides, overrideFingerAxis, GetOverridePermissions(grabOverrides), overrideValues);
        }

        protected virtual void DoControllerUngrab(object sender, ObjectInteractEventArgs e)
        {
            HandleOverrideOff(grabOverrides.ignoreAllOverrides, GetOverridePermissions(touchOverrides), false);
        }

        protected virtual void DoControllerUse(object sender, ObjectInteractEventArgs e)
        {
            bool isGrabbing = (interactGrab != null && interactGrab.GetGrabbedObject() != null);
            float[] overrideFingerAxis = (isGrabbing ? GetOverrideValues(grabOverrides) : fingerAxis);
            HandleOverrideOn(useOverrides.ignoreAllOverrides, overrideFingerAxis, GetOverridePermissions(useOverrides), GetOverrideValues(useOverrides));
        }

        protected virtual void DoControllerUnuse(object sender, ObjectInteractEventArgs e)
        {
            HandleOverrideOff(useOverrides.ignoreAllOverrides, GetOverridePermissions(useOverrides), true);
        }
        #endregion Event methods

        protected virtual void DetectController()
        {
            controllerType = VRTK_DeviceFinder.GetCurrentControllerType();
            if (controllerType != SDK_BaseController.ControllerType.Undefined)
            {
                if (autoDetectController)
                {
                    switch (controllerType)
                    {
                        case SDK_BaseController.ControllerType.SteamVR_ViveWand:
                            thumbState = VRTK_ControllerEvents.AxisType.Digital;
                            indexState = VRTK_ControllerEvents.AxisType.Axis;
                            middleState = VRTK_ControllerEvents.AxisType.Digital;
                            ringState = VRTK_ControllerEvents.AxisType.Digital;
                            pinkyState = VRTK_ControllerEvents.AxisType.Digital;
                            threeFingerState = VRTK_ControllerEvents.AxisType.Digital;
                            break;
                        case SDK_BaseController.ControllerType.Oculus_OculusTouch:
                        case SDK_BaseController.ControllerType.SteamVR_OculusTouch:
                            thumbState = VRTK_ControllerEvents.AxisType.Digital;
                            indexState = VRTK_ControllerEvents.AxisType.Axis;
                            middleState = VRTK_ControllerEvents.AxisType.Digital;
                            ringState = VRTK_ControllerEvents.AxisType.Digital;
                            pinkyState = VRTK_ControllerEvents.AxisType.Digital;
                            threeFingerState = VRTK_ControllerEvents.AxisType.Axis;
                            break;
                        case SDK_BaseController.ControllerType.SteamVR_ValveKnuckles:
                            thumbState = VRTK_ControllerEvents.AxisType.Digital;
                            indexState = VRTK_ControllerEvents.AxisType.SenseAxis;
                            middleState = VRTK_ControllerEvents.AxisType.SenseAxis;
                            ringState = VRTK_ControllerEvents.AxisType.SenseAxis;
                            pinkyState = VRTK_ControllerEvents.AxisType.SenseAxis;
                            threeFingerState = VRTK_ControllerEvents.AxisType.SenseAxis;
                            threeFingerAxisButton = SDK_BaseController.ButtonTypes.StartMenu;
                            break;
                        default:
                            thumbState = VRTK_ControllerEvents.AxisType.Digital;
                            indexState = VRTK_ControllerEvents.AxisType.Digital;
                            middleState = VRTK_ControllerEvents.AxisType.Digital;
                            ringState = VRTK_ControllerEvents.AxisType.Digital;
                            pinkyState = VRTK_ControllerEvents.AxisType.Digital;
                            threeFingerState = VRTK_ControllerEvents.AxisType.Digital;
                            break;
                    }
                }
                UnsubscribeEvents();
                SubscribeEvents();
                if (mirrorModel)
                {
                    mirrorModel = false;
                    MirrorHand();
                }
            }
        }

        protected virtual void MirrorHand()
        {
            Transform modelTransform = transform.Find("Model");
            if (modelTransform != null)
            {
                modelTransform.localScale = new Vector3(modelTransform.localScale.x * -1f, modelTransform.localScale.y, modelTransform.localScale.z);
            }
        }

        protected virtual void SetOverrideValue(int stateIndex, ref OverrideState[] overrideState, OverrideState stateValue)
        {
            overrideState[stateIndex] = stateValue;
        }

        protected virtual void ProcessFinger(VRTK_ControllerEvents.AxisType state, int arrayIndex)
        {
            if (overrideAxisValues[arrayIndex] != OverrideState.NoOverride)
            {
                if (fingerAxis[arrayIndex] != fingerForceAxis[arrayIndex])
                {
                    LerpChangePosition(arrayIndex, fingerAxis[arrayIndex], fingerForceAxis[arrayIndex], animationSnapSpeed);
                }
                else if (overrideAxisValues[arrayIndex] == OverrideState.WasOverring)
                {
                    SetOverrideValue(arrayIndex, ref overrideAxisValues, OverrideState.NoOverride);
                }
            }
            else
            {
                if (state == VRTK_ControllerEvents.AxisType.Digital)
                {
                    if (fingerChangeStates[arrayIndex])
                    {
                        fingerChangeStates[arrayIndex] = false;
                        float startAxis = (fingerStates[arrayIndex] ? 0f : 1f);
                        float targetAxis = (fingerStates[arrayIndex] ? 1f : 0f);
                        LerpChangePosition(arrayIndex, startAxis, targetAxis, animationSnapSpeed);
                    }
                }
                else
                {
                    SetFingerPosition(arrayIndex, fingerAxis[arrayIndex]);
                }
            }
        }

        protected virtual void LerpChangePosition(int arrayIndex, float startPosition, float targetPosition, float speed)
        {
            fingerAnimationRoutine[arrayIndex] = StartCoroutine(ChangePosition(arrayIndex, startPosition, targetPosition, speed));

        }

        protected virtual IEnumerator ChangePosition(int arrayIndex, float startAxis, float targetAxis, float time)
        {
            float elapsedTime = 0f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                float currentAxis = Mathf.Lerp(startAxis, targetAxis, (elapsedTime / time));
                SetFingerPosition(arrayIndex, currentAxis);
                yield return null;
            }
            SetFingerPosition(arrayIndex, targetAxis);
            fingerAnimationRoutine[arrayIndex] = null;
        }

        protected virtual void SetFingerPosition(int arrayIndex, float axis)
        {
            int animationLayer = arrayIndex + 1;
            animator.SetLayerWeight(animationLayer, axis);
            fingerAxis[arrayIndex] = axis;
            if (overrideAxisValues[arrayIndex] == OverrideState.WasOverring)
            {
                SetOverrideValue(arrayIndex, ref overrideAxisValues, OverrideState.NoOverride);
            }
        }
    }
}