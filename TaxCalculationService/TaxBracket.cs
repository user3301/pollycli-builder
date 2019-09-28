namespace TaxCalculationService
{
    public class TaxBracket
    {
        public static (uint plus, decimal charge, uint low, uint high) FirstLevelRange = (plus: 0, charge: 0M, low: 0, high: 18_200);
        public static (uint plus, decimal charge, uint low, uint high) SecondLevelRange = (plus: 0, charge: 0.19M, low: 18_201, high: 37_000);
        public static (uint plus, decimal charge, uint low, uint high) ThirdLevelRange = (plus: 3_572, charge: 0.325M, low: 37_001, high: 87_000);
        public static (uint plus, decimal charge, uint low, uint high) FourthLevelRange = (plus: 19_822, charge: 0.37M, low: 87_001, high: 180_000);
        public static (uint plus, decimal charge, uint low, uint high) FifthLevelRange = (plus: 54_232, charge: 0.45M, low: 180_000, high: uint.MaxValue);
    }
}
