using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [VaultExtensionMethod("GenerateProgressNotesPerResident", RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
        public string GenerateProgressNotesPerResident(EventHandlerEnvironment env)
        {
            //new DailyCareBackgroundOperations().GenerateProgressNotesPerResident(env.Vault, Configuration);
            return "Completed";
        }
    }
}
