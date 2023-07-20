namespace WindowsUpdateManager.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
