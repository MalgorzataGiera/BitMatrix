using System;
using System.Collections;
using System.Text;

// prostokątna macierz bitów o wymiarach m x n
// prostokątna macierz bitów o wymiarach m x n
public partial class BitMatrix
{
    private BitArray data;
   
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public bool IsReadOnly => false;

    // tworzy prostokątną macierz bitową wypełnioną `defaultValue`
    public BitMatrix(int numberOfRows, int numberOfColumns, int defaultValue = 0)
    {
        if (numberOfRows < 1 || numberOfColumns < 1)
            throw new ArgumentOutOfRangeException("Incorrect size of matrix");
        data = new BitArray(numberOfRows * numberOfColumns, BitToBool(defaultValue));
        
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;
    }

    public static int BoolToBit(bool boolValue) => boolValue ? 1 : 0;
    public static bool BitToBool(int bit) => bit != 0;

    // przeciążone konstruktory
    public BitMatrix(int numberOfRows, int numberOfColumns, params int[] bits)
    {
        data = new BitArray(numberOfRows * numberOfColumns);
        NumberOfRows = numberOfRows;
        NumberOfColumns = numberOfColumns;

        if (bits == null || bits.Length == 0)
        {
            data = new BitArray(numberOfRows * numberOfColumns);
        }
        if (bits?.Length > data.Length)
            Array.Resize(ref bits, data.Length);
        for (int i = 0; i < bits?.Length; i++)
        {
            if (bits[i] != 0)
                bits[i] = 1;
            if (bits[i] == 1)
                data[i] = true;
            if (bits[i] == 0)
                data[i] = false;
        }
    }
    public BitMatrix(int[,] bits)
    {
        if (bits == null) throw new NullReferenceException();
        if (bits.Length < 1) throw new ArgumentOutOfRangeException();
        data = new BitArray(bits.Length);
        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);

        int k = 0;
        for (int i = 0; i < bits.GetLength(0); i++)
        {
            for (int j = 0; j < bits.GetLength(1); j ++)
            {
                if (bits[i,j] != 0)
                    bits[i,j] = 1;
                if (bits[i,j] == 1)
                    data[k] = true;
                if (bits[i,j] == 0)
                    data[k] = false;
                k++;
            }
        }
    }

    public BitMatrix(bool[,] bits)
    {
        if (bits == null) throw new NullReferenceException();
        if (bits.Length < 1) throw new ArgumentOutOfRangeException();
        data = new BitArray(bits.Length);
        NumberOfRows = bits.GetLength(0);
        NumberOfColumns = bits.GetLength(1);

        int k = 0;
        for (int i = 0; i < bits.GetLength(0); i++)
        {
            for (int j = 0; j < bits.GetLength(1); j++)
            {
                if (bits[i, j])
                    data[k] = true;
                if (!bits[i, j])
                    data[k] = false;
                k++;
            }
        }
    }
}
