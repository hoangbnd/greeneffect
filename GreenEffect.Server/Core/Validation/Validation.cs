namespace MVCCore
{
    using System;

    public static class Validation
    {
        public static Validation<TServiceResult> Validate<TServiceResult>(Func<bool> condition, string parameterName, string errorMessage) where TServiceResult : BaseServiceResult
        {
            return new Validation<TServiceResult>(condition, parameterName, errorMessage);
        }
    }
}