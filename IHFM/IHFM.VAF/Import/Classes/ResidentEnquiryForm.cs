using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IHFM.VAF.Import.Classes
{
    internal class ResidentEnquiryForm
    {
        public string CompletingMemberNameAndSurname { get; set; }          //Title                     --SELECT LIST
        public string Title { get; set; }                                   //Title                     --SELECT LIST
        public string Name { get; set; }                                    //Name                      --TEXT
        public string Surname { get; set; }                                 //Surname                      --TEXT
        public string MaritalStatus { get; set; }                           //MaritalStatus             --SELECT LIST

        public string IDNumber { get; set; }                                //IDNumber                  --TEXT
        public string IDNumberSpouse { get; set; }                          //-------------------TBD-------------------
        public string ContactNumber { get; set; }                           //Cell                      --TEXT
        public string ContactNumberSpouse { get; set; }                     //-------------------TBD-------------------
        public string EmailAddress { get; set; }                            //Email                     --TEXT
        public string EmailAddressSpouse { get; set; }                      //-------------------TBD-------------------


        public bool AcknowledgeFutureComms { get; set; }                    //FutureComms               --CHECKBOX
        public string CurrentLivingAddress { get; set; }                    //LivingAddress             --TEXT
        public string Area { get; set; }                                    //Area                      --TEXT
        public string ExistingAccommodationType { get; set; }               //ExistingAccommodation     --TEXT
        public string IncomeBracket { get; set; }                           //bfQuickMode7107537        --SELECT LIST

        public bool PastSpecialTreatmentNeeded { get; set; }                //bfQuickMode1873572        --SELECT LIST
        public string PastSpecialTreatmentDetails { get; set; }             //bfQuickMode5292294        --TEXT

        public bool CurrentSpecialTreatmentNeeded { get; set; }             //bfQuickMode8978337        --SELECT LIST
        public string CurrentSpecialTreatmentDetails { get; set; }          //bfQuickMode81672          --TEXT

        public bool CurrentImpairments { get; set; }                        //bfQuickMode79440          --SELECT LIST
        public string CurrentImpairmentsDetails { get; set; }               //bfQuickMode89291          --TEXT

        public bool SubstanceDependanceFlag { get; set; }                   //bfQuickMode1490           --SELECT LIST
        public string SubstanceDependanceDetails { get; set; }              //bfQuickMode70944          --TEXT

        public bool BoarderdHistoryFlag { get; set; }                       //bfQuickMode70485          --SELECT LIST
        public bool AllergiesFlag { get; set; }                             //bfQuickMode19077          --SELECT LIST
        public string AllergiesDetails { get; set; }                        //bfQuickMode96035          --TEXT

        public DateTime ApplicationDate { get; set; }                             //submitted                 --DATETIME


        //PLACEHOLDER: FrailCare SingleRoom
        //PLACEHOLDER: FrailCare CoupleRoom

        //PLACEHOLDER: Dementia SingleRoom

        //AssistedLivingSingleRoom
        //AssistedLivingBachelor

        //IndependantLifeRightOneBedroom
        //PLACEHOLDER: Independant-LifeRight TwoBedroom
        //IndependantRentOneBedroom
        //PLACEHOLDER: Independant-Rent TwoBedroom
        public List<string> AccommodationRequired { get; set; }

        //Urgent30Days
        //Urgent12Months
        //PLACEHOLDER: Can wait 2-5 years
        //PLACEHOLDER: Long term planning
        public List<string> AccommodationUrgency { get; set; }

        public string AccommodationUrgencyDetails { get; set; }             //ExpandUrgency             --TEXT
        public string Emailsendcopy { get; set; }                           //emailsendcopy             --CHECKBOX

        public List<string> Sites { get; set; }


        ///PLACEHOLDERS
        ///
        private List<string> accommodationRequiredIdentifiers = new List<string> { "AssistedLivingBachelor", "Liferight 1 Bedroom", "AssistedLivingSingleRoom", "IndependantLifeRightOneBedroom", "IndependantRentOneBedroom" };
        //private List<string> accommodationUrgencyIdentifiers = new List<string> { "Urgent30Days", "Urgent12Months" };

        private static readonly HashSet<string> accommodationUrgencyIdentifiers = new HashSet<string> { "Urgent30Days", "Urgent12Months" };
        private static readonly HashSet<string> siteIdentifiers = new HashSet<string> { "Urgent30Days", "Urgent12Months" };

        public ResidentEnquiryForm(XDocument doc)
        {
            AccommodationRequired = new List<string>();
            AccommodationUrgency = new List<string>();
            Sites = new List<string>();

            

            var elements = doc.Descendants("subrecord")
                              .Select(item => new
                              {
                                  Name = item.Element("name")?.Value,
                                  Value = item.Element("value")?.Value
                              })
                              .Where(x => x.Name != null);

            foreach (var item in elements)
            {
                //string name = item.Element("name")?.Value;
                //string value = item.Element("value")?.Value;

                string propertyValue = item.Value ?? string.Empty;

                if (accommodationRequiredIdentifiers.Contains(item.Name))
                {
                    AccommodationRequired.Add(item.Name);
                }
                if (accommodationUrgencyIdentifiers.Contains(item.Name))
                {
                    AccommodationUrgency.Add(item.Name);
                }
                if (item.Name == "bfQuickMode3908792" && !string.IsNullOrEmpty(propertyValue))
                {
                    Sites.Add(propertyValue);
                }

                switch (item.Name)
                {
                    case "bfQuickMode2644472":
                        this.EmailAddress = propertyValue;
                        break;
                    case "bfQuickMode7543706":
                        this.ContactNumber = propertyValue;
                        break;
                    default:
                        break;
                }

                var property = typeof(ResidentEnquiryForm).GetProperty(item.Name);
                property?.SetValue(this, propertyValue);
            }

            var submittedElement = doc.Descendants("submitted").FirstOrDefault();
            if (submittedElement != null && DateTime.TryParse(submittedElement.Value, out DateTime submittedDate))
            {
                this.ApplicationDate = submittedDate;
            }
        }

    }
}
