using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
		[VaultExtensionMethod("RefreshResidentAges", RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		public string MyVaultExtensionMethod(EventHandlerEnvironment env)
		{
			new AgeBackgroundOperations().RefreshResidentAge(env.Vault, Configuration);
			return "Completed";
		}

		[VaultExtensionMethod("RefreshSiteAverageAge",RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		public string RefreshSiteAverageAge(EventHandlerEnvironment env)
        {
			new AgeBackgroundOperations().SetAverageSiteAges(env.Vault, Configuration);
			return "Completed";
        }
	}
}
