﻿// Skeleton implementation written by Joe Zachary for CS 3500, Septebmer 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

  /// <summary>
  /// (s1,t1) is an ordered pair of strings
  /// t1 depends on s1; s1 must be evaluated before t1
  ///
  /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
  /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
  /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a
  /// set, and the element is already in the set, the set remains unchanged.
  ///
  /// Given a DependencyGraph DG:
  ///
  ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
  ///        (The set of things that depend on s)
  ///
  ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
  ///        (The set of things that s depends on)
  //
  // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
  //     dependents("a") = {"b", "c"}
  //     dependents("b") = {"d"}
  //     dependents("c") = { }
  //     dependents("d") = {"d"}
  //     dependees("a") = {}
  //     dependees("b") = {"a"}
  //     dependees("c") = {"a"}
  //     dependees("d") = {"b", "d"}
  /// </summary>
  public class DependencyGraph
  {

    private Dictionary<string, ISet<string>> dependents;
    private Dictionary<string, ISet<string>> dependees;

    /// <summary>
    /// Creates an empty DependencyGraph.
    /// </summary>
    public DependencyGraph()
    {
      dependents = new Dictionary<string, ISet<string>>();
      dependees = new Dictionary<string, ISet<string>>();
      Size = 0;
    }


    /// <summary>
    /// The number of ordered pairs in the DependencyGraph.
    /// </summary>
    public int Size
    {
      get; private set;
    }


    /// <summary>
    /// The size of dependees(s).
    /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
    /// invoke it like this:
    /// dg["a"]
    /// It should return the size of dependees("a")
    /// </summary>
    public int this[string s]
    {
      get
      {
        ISet<string> result;
        if ( dependees.TryGetValue( s, out result ) )
        {
          return result.Count;
        }
        return 0;
      }
    }


    /// <summary>
    /// Reports whether dependents(s) is non-empty.
    /// </summary>
    public bool HasDependents( string s )
    {
      return GetDependents( s ).GetEnumerator().MoveNext();
    }


    /// <summary>
    /// Reports whether dependees(s) is non-empty.
    /// </summary>
    public bool HasDependees( string s )
    {
      return GetDependees(s).GetEnumerator().MoveNext();
    }


    /// <summary>
    /// Enumerates dependents(s).
    /// </summary>
    public IEnumerable<string> GetDependents( string s )
    {
      ISet<string> result;
      if ( dependents.TryGetValue( s, out result ) )
      {
        return new HashSet<string>( result );
      }
      else
      {
        return new HashSet<String>();
      }
    }

    /// <summary>
    /// Enumerates dependees(s).
    /// </summary>
    public IEnumerable<string> GetDependees( string s )
    {
      ISet<string> result;
      if ( dependees.TryGetValue( s, out result ) )
      {
        return new HashSet<string>( result );
      }
      else
      {
        return new HashSet<String>();
      }
    }


    /// <summary>
    /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
    ///
    /// <para>This should be thought of as:</para>
    ///
    ///   t depends on s
    ///
    /// </summary>
    /// <param name="s"> s must be evaluated first. T depends on S</param>
    /// <param name="t"> t cannot be evaluated until s is</param>        ///
    public void AddDependency( string s, string t )
    {
      ISet<string> deps;

      if ( !dependents.TryGetValue( s, out deps ) )
      {
        deps = new HashSet<String>();
        dependents.Add( s, deps );
      }
      if ( deps.Add( t ) ) Size++;

      if ( !dependees.TryGetValue( t, out deps ) )
      {
        deps = new HashSet<String>();
        dependees.Add( t, deps );
      }
      deps.Add( s );
    }


    /// <summary>
    /// Removes the ordered pair (s,t), if it exists
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    public void RemoveDependency( string s, string t )
    {

      ISet<string> deps;

      if ( dependents.TryGetValue( s, out deps ) )
      {
        if ( deps.Remove( t ) ) Size--;
      }
      if ( dependees.TryGetValue( t, out deps ) )
      {
        deps.Remove( s );
      }


      if ( dependents.ContainsKey( s ) && dependents[s].Count == 0 )
        dependents.Remove( s );

      if ( dependees.ContainsKey( t ) && dependees[t].Count == 0 )
        dependents.Remove( t );

    }


    /// <summary>
    /// Removes all existing ordered pairs of the form (s,r).  Then, for each
    /// t in newDependents, adds the ordered pair (s,t).
    /// </summary>
    public void ReplaceDependents( string s, IEnumerable<string> newDependents )
    {
      foreach ( String r in GetDependents( s ) )
      {
        RemoveDependency( s, r );
      }
      foreach ( String t in newDependents )
      {
        AddDependency( s, t );
      }
    }


    /// <summary>
    /// Removes all existing ordered pairs of the form (r,s).  Then, for each
    /// t in newDependees, adds the ordered pair (t,s).
    /// </summary>
    public void ReplaceDependees( string s, IEnumerable<string> newDependees )
    {
      foreach ( String r in GetDependees( s ) )
      {
        RemoveDependency( r, s );
      }
      foreach ( String t in newDependees )
      {
        AddDependency( t, s );
      }
    }

  }

}


