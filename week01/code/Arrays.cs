public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  
    /// For example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  
    /// Assume that length is a positive integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create an array of doubles with size equal to length.
        // 2. Use a loop that runs from 0 to length - 1.
        // 3. For each position i, calculate the multiple as number * (i + 1).
        //    - We use (i + 1) because the first multiple should be number itself.
        // 4. Store each calculated multiple in the array at position i.
        // 5. Return the filled array.

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  
    /// For example, if the data is List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} 
    /// and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  
    /// The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list 
    /// rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. Determine how many elements will move from the end to the front.
        //    This is exactly the value of amount.
        // 2. Calculate the starting index of the slice at the end of the list:
        //    startIndex = data.Count - amount.
        // 3. Use GetRange to copy the last 'amount' elements into a new list.
        // 4. Remove those last 'amount' elements from the original list using RemoveRange.
        // 5. Insert the saved elements at the beginning of the list using InsertRange.
        // 6. The original list is now rotated to the right.

        int startIndex = data.Count - amount;

        List<int> endSlice = data.GetRange(startIndex, amount);

        data.RemoveRange(startIndex, amount);

        data.InsertRange(0, endSlice);
    }
}
