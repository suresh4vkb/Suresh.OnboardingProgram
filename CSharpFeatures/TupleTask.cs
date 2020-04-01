namespace CSharpFeatures
{
    /// <summary>
    /// Task 1: Write a C# Method.
    /// The return type of this method should be ValueTuple<T, T, T> where T can be of any type.
    /// Use descriptive names in the return tuple for e.g. FirstName, MiddleName and LastName. Deconstruct Tuple after Method Call and Print.
    /// </summary>
    public class TupleTask
    {
        public (T1, T2, T3) GetData<T1, T2, T3>(T1 firstName, T2 middleName, T3 lastName)
        {
            return (firstName, middleName, lastName);
        }
    }
}
