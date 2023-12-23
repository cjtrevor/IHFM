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
        [PropertyCustomValue("MFiles.Property.AutoMedsOnScript")]
        public TypedValue SetAutoMedsOnScriptValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();
            Lookups lookups = new Lookups();

            List<int> addedValues = new List<int>();

            string pipes = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_MDDValues);

            foreach (string val in pipes.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (addedValues.Contains(Int32.Parse(val)))
                    continue;

                Lookup objLookup = new Lookup() {Item = Int32.Parse(val) };//GetLookupFromVal(env.Vault,Int32.Parse(val));

                lookups.Add(-1, objLookup);
                addedValues.Add(Int32.Parse(val));
            }

            calculated.SetValueToMultiSelectLookup(lookups);
            return calculated;
        }

        private Lookup GetLookupFromVal(Vault vault, int val)
        {
            MFSearchBuilder search = new MFSearchBuilder(vault);
            search.ObjType(Configuration.MDDAuto_MDDObjectId.ID);

            SearchCondition byId = new SearchCondition();
            byId.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID);
            byId.ConditionType = MFConditionType.MFConditionTypeEqual;
            byId.TypedValue.SetValue(MFDataType.MFDatatypeInteger, val);
            search.Conditions.Add(-1, byId);

            return search.FindOneEx().ToLookup();
        }
    }
}
