using System;

namespace Practice1
{
    public class Matrix
    {
        private int[,] matrix;

        public int[,] GetMatrix() => matrix;

        public Matrix(int[,] intMatrix)
        {
            if (intMatrix != null)
            {
                matrix = intMatrix;
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
                this.matrix = matrix.GetMatrix();
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
                matrix = new int[x, y];
            }
            else
            {
                throw new ArgumentOutOfRangeException
                    ("Number of columns and rows cannot be lower or equals to 0");
            }
        }

        public int this[int x, int y]
        {
            get
            {
                try
                {
                    return matrix[x, y];
                }
                catch(IndexOutOfRangeException ex)
                {
                    throw ex;
                }
            }
            set
            {
                try
                {
                    matrix[x, y] = value;
                }
                catch(IndexOutOfRangeException ex)
                {
                    throw ex;
                }
            }
        }

        public static Matrix operator *(Matrix a, int n)
        {
            if (a != null)
            {
                int xLength = a.matrix.GetLength(0);
                int yLength = a.matrix.GetLength(1);
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
                if (a.matrix.GetLength(0) == b.matrix.GetLength(0)
                    && a.matrix.GetLength(1) == b.matrix.GetLength(1))
                {
                    int xLength = a.matrix.GetLength(0);
                    int yLength = a.matrix.GetLength(1);
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
                int aXLength = a.matrix.GetLength(0);
                int aYLength = a.matrix.GetLength(1);
                int bXLength = b.matrix.GetLength(0);
                int bYLength = b.matrix.GetLength(1);

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
                int x = matrix.matrix.GetLength(0);
                int y = matrix.matrix.GetLength(1);
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

        public override string ToString()
        {
            string matrixInString = "";

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrixInString += this[i, j] + " ";
                }

                matrixInString += "\r\n";
            }

            return matrixInString;
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
                if (matrix.matrix.GetLength(0) != this.matrix.GetLength(0) 
                    || matrix.matrix.GetLength(1) != this.matrix.GetLength(1))
                {
                    return false;
                }
                for (int i = 0; i < matrix.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] != matrix[i, j])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

    }

    public class NegativeOr0ArgumentExeption : ArgumentException
    {
        public NegativeOr0ArgumentExeption() : base() { }
        public NegativeOr0ArgumentExeption(string message) : base(message) { }
        public NegativeOr0ArgumentExeption(string message, Exception inner) : base(message, inner) { }
    }
}