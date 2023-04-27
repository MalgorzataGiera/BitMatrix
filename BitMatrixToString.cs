using System;
using System.Collections;
using System.Text;
public partial class BitMatrix
{
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < NumberOfRows; i++)
        {
            for (int j = 0; j < NumberOfColumns; j++)
            {
                result += BoolToBit(data[i * NumberOfColumns + j]);
            }
            result += (Environment.NewLine);
        }
        return result;
    }


}