namespace SimpleTripleD.Modules
{
    public delegate Task OnConfigurationAssemblyScanning(Type exportType);
    
    public delegate Task OnInitialzationAssemblyScanning(Type exportType);

    public interface IAssemblyScanner
    {
        event OnConfigurationAssemblyScanning? OnConfigurationAssemblyScanning;

        event OnInitialzationAssemblyScanning? OnInitialzationAssemblyScanning;

        Task ScanInConfigurationAsync();

        Task ScanInInitializationAsync();
    }
}
