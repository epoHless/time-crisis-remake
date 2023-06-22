using System;

public class Evt
{
    protected event Action _action = delegate { };
    public virtual void Invoke() { _action?.Invoke(); }
    public virtual void AddListener(Action _listener) { _action += _listener; }
    public virtual void RemoveListener(Action _listener) { _action -= _listener; }
}

public class Evt<T>
{
    protected event Action<T> _action = delegate {  };
    public void Invoke(T _obj) { _action?.Invoke(_obj); }
    public virtual void AddListener(Action<T> _listener) { _action += _listener; }
    public virtual void RemoveListener(Action<T> _listener) { _action -= _listener; }
}

public class Evt<T1, T2>
{
    protected event Action<T1, T2> _action = delegate {  };
    public void Invoke(T1 _obj1, T2 _obj2) { _action?.Invoke(_obj1, _obj2); }
    public virtual void AddListener(Action<T1, T2> _listener) { _action += _listener; }
    public virtual void RemoveListener(Action<T1, T2> _listener) { _action -= _listener; }
}

public class Evt<T1, T2, T3>
{
    protected event Action<T1, T2, T3> _action = delegate {  };
    public void Invoke(T1 _obj1, T2 _obj2, T3 _obj3) { _action?.Invoke(_obj1, _obj2, _obj3); }
    public virtual void AddListener(Action<T1, T2, T3> _listener) { _action += _listener; }
    public virtual void RemoveListener(Action<T1, T2, T3> _listener) { _action -= _listener; }
}