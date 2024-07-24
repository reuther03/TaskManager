using TaskManager.Abstractions.Kernel;
using TaskManager.Abstractions.Kernel.Database;

namespace TaskManager.Infrastructure.Postgres;

internal class UnitOfWorkTypeRegistry
{
    // private readonly Dictionary<string, Type> _types = new();
    //
    // public void Register<T>() where T : IBaseUnitOfWork => _types[GetKey<T>()] = typeof(T);
    //
    // public Type Resolve<T>() => _types.TryGetValue(GetKey<T>(), out var type) ? type : null;
    //
    // private static string GetKey<T>() => $"{typeof(T).GetModuleName()}";

    private readonly Dictionary<string, Type> _types = new();

    public void Register<T>() where T : IBaseUnitOfWork
        => _types[GetKey<T>()] = typeof(T);

    public Type? Resolve<T>()
        => _types.GetValueOrDefault(GetKey<T>());

    private static string GetKey<T>()
        => $"{typeof(T).GetModuleName()}";
}