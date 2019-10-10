using Splat.Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.PlatformServices;
using Windows.UI.Xaml;

namespace UnoRx.Wasm
{

  public class Container
  {
    private static readonly Type _dictionaryType = typeof(ContractDictionary<>);
    private static Type GetDictionaryType(Type serviceType) => _dictionaryType.MakeGenericType(serviceType);

    public void Create(Type serviceType)
    {
      var dicType = GetDictionaryType(serviceType);
      var dic = (ContractDictionary)Activator.CreateInstance(dicType);

   


    }

    private class ContractDictionary
    {
      private readonly ConcurrentDictionary<string, IList<Func<object>>> _dictionary = new ConcurrentDictionary<string, IList<Func<object>>>();

      public bool IsEmpty => _dictionary.Count == 0;

      public bool TryRemoveContract(string contract) =>
          _dictionary.TryRemove(contract, out var _);

      public Func<object> GetFactory(string contract) =>
          GetFactories(contract)
              .LastOrDefault();

      public IEnumerable<Func<object>> GetFactories(string contract) =>
          _dictionary.TryGetValue(contract, out var collection)
          ? collection ?? Enumerable.Empty<Func<object>>()
          : Enumerable.Empty<Func<object>>();

      public void AddFactory(string contract, Func<object> factory) =>
          _dictionary.AddOrUpdate(contract, _ => new List<Func<object>> { factory }, (_, list) =>
          {
            if (list == null)
            {
              list = new List<Func<object>>();
            }

            list.Add(factory);
            return list;
          });

      public void RemoveLastFactory(string contract) =>
          _dictionary.AddOrUpdate(contract, default(IList<Func<object>>), (_, list) =>
          {
            var lastIndex = list.Count - 1;
            if (lastIndex > 0)
            {
              list.RemoveAt(lastIndex);
            }

            // TODO if list empty remove contract entirely
            // need to find how to atomically update or remove
            // https://github.com/dotnet/corefx/issues/24246
            return list;
          });
    }

    [SuppressMessage("Design", "CA1812: Unused class.", Justification = "Used in reflection.")]
    private class ContractDictionary<T> : ContractDictionary
    {
    }


  }

  public interface IViewFor<TVm> where TVm : class
  {

  }

  public class Vm { }

  public class Program
  {



    private static App _app;

    static int Main(string[] args)
    {
      Console.WriteLine("**************************");

      var container = new Container();
      //trying to reproduce ctor not found exception
      container.Create(typeof(IViewFor<Vm>));

      Console.WriteLine("Done");




#pragma warning disable CS0618 // Type or member is obsolete
      //PlatformEnlightenmentProvider.Current.EnableWasm();
#pragma warning restore CS0618 // Type or member is obsolete

      Windows.UI.Xaml.Application.Start(_ => _app = new App());

      return 0;
    }
  }
}
