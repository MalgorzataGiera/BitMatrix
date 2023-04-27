public partial class BitMatrix
{
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
                result += (BoolToBit(data[i * j + j]));
            result += (Environment.NewLine);
        }
        return result;
    }
}