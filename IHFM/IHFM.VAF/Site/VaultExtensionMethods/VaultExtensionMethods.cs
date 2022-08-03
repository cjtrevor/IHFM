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
		[VaultExtensionMethod("SetSiteNominals", RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone)]
		public string SetSiteNominals(EventHandlerEnvironment env)
		{
			new SiteBackgroundOperations().SetSiteNominals(env.Vault, Configuration);
			return "Completed";
		}
	}
}
