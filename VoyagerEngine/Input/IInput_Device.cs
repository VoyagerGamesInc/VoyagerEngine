using Silk.NET.Input;

namespace VoyagerEngine.Input
{
    public interface IInput_Device
    {
        bool WasUpdatedThisFrame { get; set; }
        bool Disconnected { get; set; }
        List<IInput_Value> FrameInputs { get; set; }
        IInput_Listener Listener { get; set; }
        IInputDevice InputDevice { get; }

        void ProcessFrame();
        void SetListener(IInput_Listener listener);
        void Update();
        void WasRemoved();
    }
    internal abstract class Input_Device<T> : IInput_Device where T : IInputDevice
    {
        public bool WasUpdatedThisFrame { get; set; }
        public bool Disconnected { get; set; }
        public IInput_Listener Listener { get; set; }
        public List<IInput_Value> FrameInputs { get; set; } = new();
        public IInputDevice InputDevice => Device;

        protected T Device;

        internal Input_Device(T device)
        {
            Device = device;
        }

        public virtual void Update()
        {

        }

        public void SetListener(IInput_Listener listener)
        {
            Listener = listener;
            listener.Devices.Add(this);
        }
        public void WasRemoved()
        {
            Listener.Devices.Remove(this);
        }
        public void ProcessFrame()
        {
            if (Listener != null && FrameInputs.Count > 0)
            {
                for (int i = 0; i < FrameInputs.Count; i++)
                {
                    Listener.OnInputEvent(FrameInputs[i]);
                }
                FrameInputs.Clear();
            }
            WasUpdatedThisFrame = false;
        }
        /*
        protected virtual bool IsControlRegistered(InputControl control)
        {
            if (RegisteredControls != null)
            {
                if (RegisteredControls.TryGetValue(control.GetType(), out string[] controlNames))
                {
                    for (int i = 0; i < controlNames.Length; i++)
                    {
                        if (controlNames[i].Equals(GetFullName(control)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        protected virtual void TranslateAction(string buttonIn, out string buttonOut)
        {
            buttonOut = buttonIn;
            if (OverrideMapping != null)
            {
                if (OverrideMapping.TryGetValue(buttonIn, out buttonIn))
                {
                    buttonOut = buttonIn;
                }
            }
        }
        protected string GetFullName(InputControl control)
        {
            string name = control.name;
            if (control.parent != null && control.parent is not InputDevice)
            {
                name = $"{GetFullName(control.parent)}.{name}";
            }
            return name;
        }
        protected virtual void ProcessButton(ButtonControl button)
        {
            TranslateAction(GetFullName(button), out string actionName);
            GetInputLog(actionName, button, GetState(button), out string log);
            AddFrameInput(new VoyagerInput_ActionPayload(actionName, GetState(button), log));
        }
        protected virtual void ProcessStick(StickControl stick)
        {
            TranslateAction(GetFullName(stick), out string actionName);
            GetInputLog(actionName, stick, VoyagerInput_Payload.States.Hold, out string log);
            AddFrameInput(new VoyagerInput_Vector2Payload(actionName, stick.value, log));
        }
        protected virtual void ProcessAxis(AxisControl axis)
        {
            TranslateAction(GetFullName(axis), out string actionName);
            GetInputLog(actionName, axis, VoyagerInput_Payload.States.Hold, out string log);
            AddFrameInput(new VoyagerInput_FloatPayload(actionName, axis.value, log));
        }
        protected virtual void ProcessAction(string actionName, ButtonControl button)
        {
            GetInputLog(actionName, button, GetState(button), out string log);
            AddFrameInput(new VoyagerInput_ActionPayload(actionName, GetState(button), log));
        }
        protected void AddFrameInput(VoyagerInput_Payload payload)
        {
            frameInputs.Add(payload);
            WasUpdatedThisFrame = true;
        }
        protected static VoyagerInput_Payload.States GetState(ButtonControl button)
        {
            VoyagerInput_Payload.States state = VoyagerInput_Payload.States.Hold;
            if (button.wasPressedThisFrame)
                state = VoyagerInput_Payload.States.Pressed;
            else if (button.wasReleasedThisFrame)
                state = VoyagerInput_Payload.States.Released;
            return state;
        }

        protected void GetInputLog(string actionName, InputControl control, VoyagerInput_Payload.States state, out string log)
        {
#if VOYAGER_INPUT_LOG
           log = $" {LogGreen(GetType().Name)} <{LogYellow(BaseDevice.displayName)}>  -  {LogCyan(actionName)} <{LogYellow(GetFullName(control))}> - {LogCyan(state.ToString())} [{LogMagenta(control.GetType().Name)}]";
#else
            log = string.Empty;
#endif
        }
        internal override string ToString()
        {
            return $"{GetType().Name} - [{(Listener != null ? Listener.Name : "null")}]";
        }
        
        protected async void Init()
        {
            InputSystem.ResetDevice(Device);
            await Task.Delay(1000);
            InputSystem.ResetDevice(Device);
            Disconnected = false;
        }
        internal override void Update()
        {
            if(Device != null)
            {
                if (Listener != null)
                {
                    foreach (InputControl control in Device.allControls)
                    {
                        UpdateControl(control);
                    }
                }
                else 
                {
                    WasUpdatedThisFrame = Device.wasUpdatedThisFrame;
                }
            }
        }
        protected virtual void DebugControl(InputControl control)
        {
            switch (control)
            {
                case ButtonControl button:
                    if (button.wasPressedThisFrame || button.isPressed || button.wasReleasedThisFrame)
                    {
                        Debug.Log($"{GetFullName(button)}\n{button}");
                    }
                    break;
                case StickControl stick:
                    if (stick.magnitude != 0.0f)
                    {
                        Debug.Log($"{GetFullName(stick)}\n{stick}");
                    }
                    break;
                case AxisControl axis:
                    if (axis.magnitude != 0.0f)
                    {
                        Debug.Log($"{GetFullName(axis)}\n{axis}");
                    }
                    break;
            }*/
    }
}
