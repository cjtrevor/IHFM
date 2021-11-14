﻿using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public class ResidentAutomaticValueService
    {
        private Configuration _configuration;

        public ResidentAutomaticValueService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public double CalculateActualAmountOutstanding(ObjVerEx objVerEx)
        {
            if (!objVerEx.HasValue(_configuration.RoomTariff))
                return 0;

            double amount = 0;
            double tariff = double.Parse(objVerEx.GetProperty(_configuration.RoomTariff).GetValueAsLocalizedText());
            double discountPercentage = objVerEx.HasValue(_configuration.DiscountPercentage) ?
                                            objVerEx.GetProperty(_configuration.DiscountPercentage).GetValue<double>() :
                                            0 ;
            double discountRandValue = objVerEx.HasValue(_configuration.DiscountRandValue) ?
                                            objVerEx.GetProperty(_configuration.DiscountRandValue).GetValue<double>() :
                                            0 ;

            //if (discountPercentage > 0 && discountRandValue > 0)
            //{
            //    throw new Exception("You should only specify a discount percentage or a discount amount. Both are currently specified.");
            //}

            if (discountPercentage > 0)
                amount = tariff * (100 - discountPercentage) / 100;
            else if (discountRandValue > 0)
                amount = tariff - discountRandValue;
            else
                amount = tariff;

            return amount;
        }

        public double GetTariffVariance(ObjVerEx objVerEx)
        {
            if (!objVerEx.HasValue(_configuration.RoomTariff))
                return 0;

            double tariff = double.Parse(objVerEx.GetProperty(_configuration.RoomTariff).GetValueAsLocalizedText());
            double actualAmount = CalculateActualAmountOutstanding(objVerEx);

            return tariff - actualAmount;
        }
    }
}
