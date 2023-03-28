using MFiles.VAF.Configuration.AdminConfigurations;
using MFiles.VAF.Configuration.JsonEditor;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class ClassTypesOptionsProvider : IStableValueOptionsProvider
    {
        public ClassTypesOptionsProvider()
        {

        }
        public IEnumerable<ValueOption> GetOptions(IConfigurationRequestContext context)
        {
            foreach (ObjectClassAdmin objectClass in context.Vault.ClassOperations.GetAllObjectClassesAdmin())
            {
                yield return new ValueOption
                {
                    Label = objectClass.Name,
                    Value = objectClass.ID
                };
            }
        }
    }
}
