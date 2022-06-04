using System;
using System.Collections.Generic;

namespace PvaConverters.Mtrx
{
    [Serializable]
    public class Matrix
    {
        private List<double> m_data = new List<double>();
        private int          m_rows;
        private int          m_columns;

        internal Matrix(double[] array, int rows, int columns)
        {
            for (int index = 0; index < array.Length; ++index)
            {
                m_data.Add(array[index]);
            }

            m_rows = rows;
            m_columns = columns;
        }

        public Matrix(Matrix mat)
        {
            m_data = new List<double>(mat.Data);
            m_rows = mat.RowCount;
            m_columns = mat.ColumnCount;
        }

        public Matrix(double[,] matrix)
        {
            m_rows = matrix != null ? matrix.GetLength(0) : throw new ArgumentNullException(nameof(matrix));
            m_columns = matrix.GetLength(1);
            m_data = new List<double>(m_rows * m_columns);
            for (int index = 0; index < m_data.Capacity; ++index)
            {
                m_data.Add(0.0);
            }

            for (int index1 = 0; index1 < m_rows; ++index1)
            {
                for (int index2 = 0; index2 < m_columns; ++index2)
                {
                    m_data[m_rows * index2 + index1] = matrix[index1, index2];
                }
            }
        }

        public Matrix(int rows, int columns)
        {
            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            m_data = columns >= 1 ? new List<double>(rows * columns) : throw new ArgumentOutOfRangeException(nameof(columns));
            for (int index = 0; index < m_data.Capacity; ++index)
            {
                m_data.Add(0.0);
            }

            m_rows = rows;
            m_columns = columns;
        }

        public int RowCount
        {
            get => m_rows;
            set => m_rows = value;
        }

        public int ColumnCount
        {
            get => m_columns;
            set => m_columns = value;
        }

        public double[] Data => m_data.ToArray();

        public double[,] DataArray
        {
            get
            {
                double[,] dataArray = new double[RowCount, ColumnCount];
                for (int row = 0; row < RowCount; ++row)
                {
                    for (int column = 0; column < ColumnCount; ++column)
                    {
                        dataArray[row, column] = GetEntry(row, column);
                    }
                }

                return dataArray;
            }
        }

        public double this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= RowCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(row));
                }

                if (column < 0 || column >= ColumnCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(column));
                }

                return m_data[m_rows * column + row];
            }
            set
            {
                if (row < 0 || row >= RowCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(row));
                }

                if (column < 0 || column >= ColumnCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(column));
                }

                m_data[m_rows * column + row] = value;
            }
        }

        internal double GetEntry(int row, int column) => m_data[m_rows * column + row];

        internal void SetEntry(int row, int column, double value) => m_data[m_rows * column + row] = value;

        public Matrix Transpose()
        {
            double[] transposed = new double[m_data.Count];
            for (int index1 = 0; index1 < RowCount; ++index1)
            {
                for (int index2 = 0; index2 < ColumnCount; ++index2)
                {
                    transposed[ColumnCount * index1 + index2] = m_data[RowCount * index2 + index1];
                }
            }

            return new Matrix(transposed, RowCount, ColumnCount);
        }

        public Matrix Invert()
        {
            if (ColumnCount != RowCount)
            {
                throw new Exception("cannot invert non symmetric matrix");
            }

            double[,] inverted = MatrixAlgo.Invert(DataArray);
            return new Matrix(inverted);
        }
    }
}