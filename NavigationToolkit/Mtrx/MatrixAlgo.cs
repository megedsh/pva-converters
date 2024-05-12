using System;

namespace NavigationToolkit.Mtrx
{
    public static class MatrixAlgo
    {
        /// <summary>
        ///     Calculate the inverse of a matrix using Gauss-Jordan elimination
        /// </summary>
        /// <param name="matrix">Matrix to invert</param>
        /// <returns>Inverted matrix</returns>
        public static double[,] Invert(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix");
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int n, r;
            double scale;

            // Validate the matrix size
            if (rows != cols)
            {
                throw new ArgumentException("Given matrix is not a square!", "matrix");
            }

            n = rows;
            double[,] inverse = new double[rows, cols];

            // Initialize the inverse to the identity
            for (r = 0; r < n; ++r)
            {
                for (int c = 0; c < n; ++c)
                {
                    inverse[r, c] = r == c ? 1.0 : 0.0;
                }
            }

            // Process the matrix one column at a time
            for (int c = 0; c < n; ++c)
            {
                // Scale the current row to start with 1
                // Swap rows if the current value is too close to 0.0
                if (Math.Abs(matrix[c, c]) <= 2.0 * double.Epsilon)
                {
                    for (r = c + 1; r < n; ++r)
                    {
                        if (Math.Abs(matrix[r, c]) <= 2.0 * double.Epsilon)
                        {
                            rowSwap(matrix, n, c, r);
                            rowSwap(inverse, n, c, r);
                            break;
                        }
                    }

                    if (r >= n)
                    {
                        throw new Exception("Given matrix is singular!");
                    }
                }

                scale = 1.0 / matrix[c, c];
                rowScale(matrix, n, scale, c);
                rowScale(inverse, n, scale, c);

                // Zero out the rest of the column
                for (r = 0; r < n; ++r)
                {
                    if (r != c)
                    {
                        scale = -matrix[r, c];
                        rowScaleAdd(matrix, n, scale, c, r);
                        rowScaleAdd(inverse, n, scale, c, r);
                    }
                }
            }

            return inverse;
        }

        /// <summary>
        ///     Swap 2 rows in a matrix
        /// </summary>
        /// <param name="matrix">The matrix to operate on</param>
        /// <param name="n">The size of the matrix</param>
        /// <param name="r0">First row to swap</param>
        /// <param name="r1">Second row to swap</param>
        private static void rowSwap(double[,] matrix, int n, int r0, int r1)
        {
            double tmp;

            for (int i = 0; i < n; ++i)
            {
                tmp = matrix[r0, i];
                matrix[r0, i] = matrix[r1, i];
                matrix[r1, i] = tmp;
            }
        }

        /// <summary>
        ///     Perform scale and add a row in a matrix to another row
        /// </summary>
        /// <param name="matrix">The matrix to operate on</param>
        /// <param name="n">The size of the matrix</param>
        /// <param name="a">The scale factor to apply to row <paramref name="r0" /></param>
        /// <param name="r0">The row to scale</param>
        /// <param name="r1">The row to add and store to</param>
        private static void rowScaleAdd(double[,] matrix, int n, double a, int r0, int r1)
        {
            for (int i = 0; i < n; ++i)
            {
                matrix[r1, i] += a * matrix[r0, i];
            }
        }

        /// <summary>
        ///     Scale a row in a matrix by a constant factor
        /// </summary>
        /// <param name="matrix">The matrix to operate on</param>
        /// <param name="n">The size of the matrix</param>
        /// <param name="a">The factor to scale row <paramref name="r" /></param>
        /// <param name="r">The row to scale</param>
        private static void rowScale(double[,] matrix, int n, double a, int r)
        {
            for (int i = 0; i < n; ++i)
            {
                matrix[r, i] *= a;
            }
        }

        public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null)
            {
                throw new ArgumentNullException(nameof(matrix1));
            }

            if (matrix2 == null)
            {
                throw new ArgumentNullException(nameof(matrix2));
            }

            if (matrix1.ColumnCount != matrix2.RowCount)
            {
                throw new Exception("Dimension Mismatch");
            }

            Matrix matrix = new Matrix(matrix1.RowCount, matrix2.ColumnCount);
            for (int row = 0; row < matrix.RowCount; ++row)
            {
                for (int column = 0; column < matrix.ColumnCount; ++column)
                {
                    matrix[row, column] = 0.0;
                    for (int index = 0; index < matrix1.ColumnCount; ++index)
                    {
                        matrix[row, column] += matrix1[row, index] * matrix2[index, column];
                    }
                }
            }

            return matrix;
        }
    }
}