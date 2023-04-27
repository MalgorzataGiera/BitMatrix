// konstruktor BitMatrix(bool[,])
bool[,] arr = new bool[,] { { true, false, true }, { false, true, true } };
var m = new BitMatrix(arr);
Console.WriteLine(arr.GetLength(0) == m.NumberOfRows);
Console.WriteLine(arr.GetLength(1) == m.NumberOfColumns);
Console.Write(m.ToString());