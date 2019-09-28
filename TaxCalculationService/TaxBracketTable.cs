﻿using Models;

namespace TaxCalculationService
{
    public class TaxBracketTable
    {
        public static TaxPlan FirstLevel = new TaxPlan { SurCharge = 0M, TaxBase = 0U, LowerBound = 0U, UpperBound = 18_200U };
        public static TaxPlan SecondLevel = new TaxPlan { SurCharge = 0.19M, TaxBase = 0U, LowerBound = 18_201U, UpperBound = 37_000U };
        public static TaxPlan ThirdLevel = new TaxPlan { SurCharge = 0.325M, TaxBase = 3_572U, LowerBound = 37_001U, UpperBound = 87_000U };
        public static TaxPlan FourthLevel = new TaxPlan { SurCharge = 0.37M, TaxBase = 19_822U, LowerBound = 87_001U, UpperBound = 180_000U };
        public static TaxPlan FifthLevel = new TaxPlan { SurCharge = 0.45M, TaxBase = 54_232U, LowerBound = 180_001U, UpperBound = uint.MaxValue };
    }
}