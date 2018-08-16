
using UnityEngine;

    public abstract class BaseModule
    {
        public enum EnumRegistMode
    {
        NotRegister,
        AutoRegister,
        AlradyRegister,
    }
    private EnumObjectState state = EnumObjectState.Initial;
    public event StateChangedEvent StateChanged;
    
    public EnumObjectState State
    {
        get { return state; }
        set
        {
            if (state == value) return;
            EnumObjectState oldState = state;
            state = value;
            if (null != StateChanged)
            {
                StateChanged(this, state, oldState);
            }
            OnStateChanged(state,oldState);
        }
    }

    protected virtual void OnStateChanged(EnumObjectState newState,EnumObjectState oldState)
    {
        
    }

    private EnumRegistMode registerMode = EnumRegistMode.NotRegister;

    public bool AutoRegister
    {
        get
        {
            return registerMode == EnumRegistMode.NotRegister ? false : true; 
            
        }
        set
        {
            if (registerMode == EnumRegistMode.NotRegister || registerMode == EnumRegistMode.AutoRegister)
            {
                registerMode = value ? EnumRegistMode.AutoRegister : EnumRegistMode.NotRegister;
            }
        }
    }

    public bool HasRegistered
    {
        get { return registerMode == EnumRegistMode.AlradyRegister; }
    }
    public void Release()
    {
        if (State != EnumObjectState.Disabled)
        {
            State = EnumObjectState.Disabled;
            //TODO:
            if (registerMode == EnumRegistMode.AlradyRegister)
            {
                //TODO:注销
                registerMode = EnumRegistMode.AutoRegister;
            }
            OnRelease();
        }
    }

    protected virtual void OnRelease()
    {
        
    }

    public void Load()
    {
        if (State != EnumObjectState.Initial) return;
        State = EnumObjectState.Loading;
        //TODO:
        if (registerMode == EnumRegistMode.AutoRegister)
        {
            // TODO:注册
            registerMode = EnumRegistMode.AlradyRegister;
        }
        OnLoad();
        State = EnumObjectState.Ready;
    }

    protected virtual void OnLoad()
    {
        
    }
    }
