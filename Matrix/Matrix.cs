using System;

namespace Practice1
{
    public class Matrix
    {
        public int[,] MatrixValue { get; private set; }

        public Matrix()
        {
            MatrixValue = new int[0,0];
        }

        public Matrix(int[,] intMatrix)
        {
            if (intMatrix != null)
            {
                MatrixValue = intMatrix;
            }
            else
            {
                throw new ArgumentNullException("Matrix cannot be null");
            }
        }

        public Matrix(Matrix matrix)
        {
            if (matrix != null)
            {
                MatrixValue = matrix.MatrixValue;
            }
            else
            {
                throw new ArgumentNullException("Matrix cannot be null");
            }
        }
        public Matrix(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                MatrixValue = new int[x, y];
            }
            else
            {
                throw new ArgumentOutOfRangeException
                    ("Number of columns and rows cannot be lower or equals to 0");
            }
        }

        public int this[int x, int y]
        {
            get => MatrixValue[x, y];
            set => MatrixValue[x, y] = value;
        }

        public static Matrix operator *(Matrix a, int n)
        {
            if (a != null)
            {
                int xLength = a.MatrixValue.GetLength(0);
                int yLength = a.MatrixValue.GetLength(1);
                Matrix result = new Matrix(xLength, yLength);
                for (int i = 0; i < xLength; i++)
                {
                    for (int j = 0; j < yLength; j++)
                    {
                        result[i, j] = a[i, j] * n;
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a != null || b != null)
            {
                if (a.MatrixValue.GetLength(0) == b.MatrixValue.GetLength(0)
                    && a.MatrixValue.GetLength(1) == b.MatrixValue.GetLength(1))
                {
                    int xLength = a.MatrixValue.GetLength(0);
                    int yLength = a.MatrixValue.GetLength(1);
                    Matrix sum = new Matrix(xLength, yLength);
                    for (int i = 0; i < xLength; i++)
                    {
                        for (int j = 0; j < yLength; j++)
                        {
                            sum[i, j] = a[i, j] + b[i, j];
                        }
                    }

                    return sum;
                }
                else
                {
                    throw new ArgumentException("Both matrices must be the same length and width");
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a != null || b != null)
            {
                return new Matrix(a + (b * -1));
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a != null || b != null)
            {
                int aXLength = a.MatrixValue.GetLength(0);
                int aYLength = a.MatrixValue.GetLength(1);
                int bXLength = b.MatrixValue.GetLength(0);
                int bYLength = b.MatrixValue.GetLength(1);

                if (aYLength == bXLength)
                {
                    Matrix result = new Matrix(aXLength, bYLength);
                    for (int i = 0; i < aXLength; i++)
                    {
                        for (int j = 0; j < bYLength; j++)
                        {
                            int intermediateResult = 0;
                            for (int k = 0; k < aYLength; k++)
                            {
                                intermediateResult += a[i, k] * b[k, j];
                            }

                            result[i, j] = intermediateResult;
                        }
                    }

                    return result;
                }
                else
                {
                    throw new ArgumentException(
                        "The number of columns in the first matrix must be equals to the number of rows in second one");
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static Matrix TransposeMatrix(Matrix matrix)
        {
            if (matrix != null)
            {
                int x = matrix.MatrixValue.GetLength(0);
                int y = matrix.MatrixValue.GetLength(1);
                Matrix result = new Matrix(x, y);

                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        result[i, j] = matrix[j, i];
                    }
                }

                return result;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void PrintMatrix()
        {
            string matrixInString = "";

            for (int i = 0; i < MatrixValue.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixValue.GetLength(1); j++)
                {
                    matrixInString += this[i, j] + " ";
                }

                matrixInString += "\r\n";
            }

            Console.WriteLine(matrixInString);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType() )
            {
                return false;
            }
            else
            {
                Matrix matrix = (Matrix) obj;
                if (matrix.MatrixValue.GetLength(0) != MatrixValue.GetLength(0) 
                    || matrix.MatrixValue.GetLength(1) != MatrixValue.GetLength(1))
                {
                    return false;
                }
                for (int i = 0; i < matrix.MatrixValue.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.MatrixValue.GetLength(1); j++)
                    {
                        if (matrix[i, j] != MatrixValue[i, j])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public Matrix Copy()
        {
            return new Matrix(this);
        }
    }
}