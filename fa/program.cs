using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fans
{
  public class State
  {
    public string Name;
    public Dictionary<char, State> Transitions;
    public bool IsAcceptState;
  }


  // FA1: accepts binary strings with exactly one '0' and at least one '1'
  public class FA1
  {
    State qStart, qHas1, qHas0, qHas01, qDead;
    State InitialState;

    public FA1()
    {
      qStart = new State { Name = "start", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qHas1  = new State { Name = "has1",  IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qHas0  = new State { Name = "has0",  IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qHas01 = new State { Name = "has01", IsAcceptState = true,  Transitions = new Dictionary<char, State>() };
      qDead  = new State { Name = "dead",  IsAcceptState = false, Transitions = new Dictionary<char, State>() };

      qStart.Transitions['0'] = qHas0;
      qStart.Transitions['1'] = qHas1;
      qHas1.Transitions['0']  = qHas01;
      qHas1.Transitions['1']  = qHas1;
      qHas0.Transitions['0']  = qDead;
      qHas0.Transitions['1']  = qHas01;
      qHas01.Transitions['0'] = qDead;
      qHas01.Transitions['1'] = qHas01;
      qDead.Transitions['0']  = qDead;
      qDead.Transitions['1']  = qDead;

      InitialState = qStart;
    }

    public bool? Run(IEnumerable<char> s)
    {
      State current = InitialState;
      foreach (var c in s)
      {
        if (!current.Transitions.ContainsKey(c))
          return null;
        current = current.Transitions[c];
      }
      return current.IsAcceptState;
    }
  }

  // FA2: accepts binary strings with odd count of '0' and odd count of '1'
  public class FA2
  {
    State qEE, qEO, qOE, qOO;
    State InitialState;

    public FA2()
    {
      qEE = new State { Name = "ee", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qEO = new State { Name = "eo", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qOE = new State { Name = "oe", IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qOO = new State { Name = "oo", IsAcceptState = true,  Transitions = new Dictionary<char, State>() };

      qEE.Transitions['0'] = qOE;
      qEE.Transitions['1'] = qEO;
      qEO.Transitions['0'] = qOO;
      qEO.Transitions['1'] = qEE;
      qOE.Transitions['0'] = qEE;
      qOE.Transitions['1'] = qOO;
      qOO.Transitions['0'] = qEO;
      qOO.Transitions['1'] = qOE;

      InitialState = qEE;
    }

    public bool? Run(IEnumerable<char> s)
    {
      State current = InitialState;
      foreach (var c in s)
      {
        if (!current.Transitions.ContainsKey(c))
          return null;
        current = current.Transitions[c];
      }
      return current.IsAcceptState;
    }
  }

  // FA3: accepts binary strings containing '11' as a substring
  public class FA3
  {
    State qInit, qOne1, qAccept;
    State InitialState;

    public FA3()
    {
      qInit   = new State { Name = "init",   IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qOne1   = new State { Name = "one1",   IsAcceptState = false, Transitions = new Dictionary<char, State>() };
      qAccept = new State { Name = "accept", IsAcceptState = true,  Transitions = new Dictionary<char, State>() };

      qInit.Transitions['0']   = qInit;
      qInit.Transitions['1']   = qOne1;
      qOne1.Transitions['0']   = qInit;
      qOne1.Transitions['1']   = qAccept;
      qAccept.Transitions['0'] = qAccept;
      qAccept.Transitions['1'] = qAccept;

      InitialState = qInit;
    }

    public bool? Run(IEnumerable<char> s)
    {
      State current = InitialState;
      foreach (var c in s)
      {
        if (!current.Transitions.ContainsKey(c))
          return null;
        current = current.Transitions[c];
      }
      return current.IsAcceptState;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      String s = "01111";
      FA1 fa1 = new FA1();
      bool? result1 = fa1.Run(s);
      Console.WriteLine(result1);
      FA2 fa2 = new FA2();
      bool? result2 = fa2.Run(s);
      Console.WriteLine(result2);
      FA3 fa3 = new FA3();
      bool? result3 = fa3.Run(s);
      Console.WriteLine(result3);
    }
  }
}
