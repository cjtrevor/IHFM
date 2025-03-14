using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [VaultExtensionMethod("ImportResidentFormData", RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
        public string ImportResidentFormData(EventHandlerEnvironment env)
        {
            new ResidentBackgroundOperations().ImportResidentFormData(env.Vault, Configuration);
            return "Completed";
        }
    }
}
