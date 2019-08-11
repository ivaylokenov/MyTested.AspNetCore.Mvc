namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    public enum CustomEnum
    {
        DefaultConstant,
        ConstantWithCustomValue = 128,
        CombinedConstant = DefaultConstant | ConstantWithCustomValue
    }
}
