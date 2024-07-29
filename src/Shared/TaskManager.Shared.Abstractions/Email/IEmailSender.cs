using TaskManager.Abstractions.Kernel.Primitives;

namespace TaskManager.Abstractions.Email;

public interface IEmailSender
{
    public Task Send(EmailMessage request);
}