using TaskManager.Abstractions.Kernel.Primitives;
using TaskManager.Abstractions.Kernel.ValueObjects;
using TaskManager.Modules.Managment.Domain.Groups.ValueObjects;

namespace TaskManager.Modules.Managment.Domain.Groups.Entities;

public class Groups : AggregateRoot<GroupId>
{
    public Name GroupName { get; private set; }
    //list id userow?
    //lista id taskow?
}