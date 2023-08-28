using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Util
{
  public class Coroutiner
  {
    public Coroutine current;

    private Func<IEnumerator> routine;

    public Coroutiner(Func<IEnumerator> routine)
    {
      this.routine = routine;
    }

    public void Start(params object[] parameters)
    {
      Stop();
      current = GameManager.Instance.StartCoroutine(routine.Invoke());
    }

    public void Stop()
    {
      if (current is not null)
        GameManager.Instance.StopCoroutine(current);
    }
  }

  public class Coroutiner<T1>
  {
    public delegate IEnumerator Fn(T1 param1);

    public Coroutine current;

    private Fn routine;

    public Coroutiner(Fn routine)
    {
      this.routine = routine;
    }

    public void Start(T1 param1)
    {
      Stop();
      current = GameManager.Instance.StartCoroutine(routine.Invoke(param1));
    }

    public void Stop()
    {
      if (current is not null)
        GameManager.Instance.StopCoroutine(current);
    }
  }

  public class Coroutiner<T1, T2>
  {
    public delegate IEnumerator Fn(T1 param1, T2 param2);

    public Coroutine current;

    private Fn routine;

    public Coroutiner(Fn routine)
    {
      this.routine = routine;
    }

    public void Start(T1 param1, T2 param2)
    {
      Stop();
      current = GameManager.Instance.StartCoroutine(routine.Invoke(param1, param2));
    }

    public void Stop()
    {
      if (current is not null)
        GameManager.Instance.StopCoroutine(current);
    }
  }

  public class Coroutiner<T1, T2, T3>
  {
    public delegate IEnumerator Fn(T1 param1, T2 param2, T3 param3);

    public Coroutine current;

    private Fn routine;

    public Coroutiner(Fn routine)
    {
      this.routine = routine;
    }

    public void Start(T1 param1, T2 param2, T3 param3)
    {
      Stop();
      current = GameManager.Instance.StartCoroutine(routine.Invoke(param1, param2, param3));
    }

    public void Stop()
    {
      if (current is not null)
        GameManager.Instance.StopCoroutine(current);
    }
  }

  public class Coroutiner<T1, T2, T3, T4>
  {
    public delegate IEnumerator Fn(T1 param1, T2 param2, T3 param3, T4 param4);

    public Coroutine current;

    private Fn routine;

    public Coroutiner(Fn routine)
    {
      this.routine = routine;
    }

    public void Start(T1 param1, T2 param2, T3 param3, T4 param4)
    {
      Stop();
      current = GameManager.Instance.StartCoroutine(routine.Invoke(param1, param2, param3, param4));
    }

    public void Stop()
    {
      if (current is not null)
        GameManager.Instance.StopCoroutine(current);
    }
  }
}
