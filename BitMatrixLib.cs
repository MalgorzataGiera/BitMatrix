using System;
using System.Collections;
using System.Text;


// prostokątna macierz bitów o wymiarach m x n
// prostokątna macierz bitów o wymiarach m x n
public class BitMatrix : IEquatable<BitMatrix>, IEnumerable<int>, ICloneable
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
            for (int j = 0; j < bits.GetLength(1); j++)
            {
                if (bits[i, j] != 0)
                    bits[i, j] = 1;
                if (bits[i, j] == 1)
                    data[k] = true;
                if (bits[i, j] == 0)
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
    public bool Equals(BitMatrix other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other == null || this == null) return false;
        if (NumberOfRows != other.NumberOfRows || NumberOfColumns != other.NumberOfColumns) return false;

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] != other.data[i]) return false;

        }
        return true;

    }
    public override bool Equals(object obj)
    {
        return Equals(obj as BitMatrix);
    }
    public override int GetHashCode()
    {
        return GetHashCode();
    }

    public static bool operator ==(BitMatrix m1, BitMatrix m2)
    {
        if (ReferenceEquals(m1, m2)) return true;
        if ((object)m1 == null || (object)m2 == null) return false;
        return m1.Equals(m2);
    }
    public static bool operator !=(BitMatrix m1, BitMatrix m2) => !(m1 == m2);

    // indexer
    public int this[int row, int col]
    {
        get
        {
            if (row > NumberOfRows - 1 || col > NumberOfColumns - 1) throw new IndexOutOfRangeException();
            if (row < 0 || col < 0) throw new IndexOutOfRangeException();
            return BoolToBit(data[row * NumberOfColumns + col]);
        }

        set
        {
            if (row > NumberOfRows - 1 || col > NumberOfColumns - 1) throw new IndexOutOfRangeException();
            if (row < 0 || col < 0) throw new IndexOutOfRangeException();
            if (value != 0) data[row * NumberOfColumns + col] = BitToBool(1);
            else data[row * NumberOfColumns + col] = BitToBool(0);
        }
    }
    // numerator
    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < data.Length; i++)
        {
            yield return BoolToBit(data[i]);
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public BitMatrix Clone()
    {
        var result = new BitMatrix(NumberOfRows, NumberOfColumns);
        for (int i = 0; i < data.Length; i++)
            result.data[i] = this.data[i];
        return result;
    }
    object ICloneable.Clone() => Clone();

    public static BitMatrix Parse(string s)
    {
        if (String.IsNullOrEmpty(s)) throw new ArgumentNullException();
        string[] splitted = s.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);
        if (splitted.Length < 0) throw new FormatException();
        var result = new BitMatrix(splitted.Length, splitted[0].Length);

        for (int i = 0; i < splitted.Length; i++)
        {
            if (splitted[i].Length != splitted[0].Length) throw new FormatException();
            for (int j = 0; j < splitted[0].Length; j++)
            {
                char c = splitted[i][j];
                if (c != '0' && c != '1') throw new FormatException();
                result[i, j] = c == '1' ? 1 : 0;
            }
        }
        return result;
    }

    public static bool TryParse(string s, out BitMatrix result)
    {
        result = new BitMatrix(2, 2);
        
        if (s == null) return false;
        string[] splitted = s.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);
        if (splitted.Length < 0) return false;
        result = new BitMatrix(splitted.Length, splitted[0].Length);

        for (int i = 0; i < splitted.Length; i++)
        {
            if (splitted[i].Length != splitted[0].Length) return false;
            for (int j = 0; j < splitted[0].Length; j++)
            {
                char c = splitted[i][j];
                if (c != '0' && c != '1') return false;
                result[i, j] = c == '1' ? 1 : 0;
            }
        }
        return true;
    }
}