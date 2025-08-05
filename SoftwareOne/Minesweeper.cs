namespace SoftwareOne;

public class Minesweeper
{
    // an array of tuples that gives the relative index locations of a particular array element's neighbouring elements
    private static (int neighbourRowModifier, int neighbourColumnModifier)[] s_neighbourMap =
        [
            // row above
            (-1, -1), // left
            (-1, 0), // center
            (-1, 1), // right

            // same row 
            (0, -1), // left
            (0, 1), // right

            // row below
            (1, -1), // left
            (1, 0), // center
            (1, 1) // right
        ];

    public void Run(int boardIndex)
    {
        // step 1 - Get a new board
        string[,] board = GetBoard(boardIndex);
        int rowCount = board.GetLength(0);
        int colCount = board.GetLength(1);

        // step 2 - initialise a results array (mineProximityCount) with zeros
        int[,] mineProximityCount = new int[rowCount, colCount];
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                mineProximityCount[rowIndex, colIndex] = 0;
            }
        }

        // step 3 - scan for mines and track the touchpoints
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                if (board[rowIndex, colIndex] == "*") // we have found a mine - increment the mineProximityCount array value for all neighbours of this location
                {
                    foreach ((int x, int y) in GetValidNeighbours(rowCount, colCount, rowIndex, colIndex))
                    {
                        mineProximityCount[x, y]++;
                    }
                }
            }
        }

        // step 4 - copy results into the main board and display
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                if (board[rowIndex, colIndex] == ".")
                {
                    board[rowIndex, colIndex] = mineProximityCount[rowIndex, colIndex].ToString();
                }

                Console.Write($"{board[rowIndex, colIndex]} ");
            }

            Console.WriteLine();
        }
    }

    // use the s_neighbourMap to get the index values of the neighbouring array elements of [currentRowIndex][currentColIndex]
    private static IEnumerable<(int neighbourRowIndex, int neighbourColIndex)> GetValidNeighbours(int rowCount, int colCount, int currentRowIndex, int currentColIndex)
    {
        List<(int neighbourRowIndex, int neighbourColIndex)> neighbourIndices = new();

        int candidateNeighbourRowIndex, candidateNeighbourColIndex;
        for (int neighbourMapIndex = 0; neighbourMapIndex < s_neighbourMap.Length; neighbourMapIndex++)
        {
            (int rowModifier, int colModifier) = s_neighbourMap[neighbourMapIndex];
            candidateNeighbourRowIndex = currentRowIndex + rowModifier;
            candidateNeighbourColIndex = currentColIndex + colModifier;

            // add the candidate neighbour's indices to the collection as long as they are valid within the grid defined by rowCount and colCount
            if (candidateNeighbourRowIndex >= 0 && candidateNeighbourRowIndex < rowCount && candidateNeighbourColIndex >= 0 && candidateNeighbourColIndex < colCount)
            {
                neighbourIndices.Add((candidateNeighbourRowIndex, candidateNeighbourColIndex));
            }
        }

        return neighbourIndices;
    }

    private string[,] GetBoard(int boardIndex)
    {
        switch (boardIndex)
        {
            case 0:
                {
                    return new string[,]
                    {
                        { "*", ".", ".", "." },
                        { ".", ".", ".", "." },
                        { ".", "*", ".", "." },
                        { ".", ".", ".", "." }
                    };
                }

            case 1:
                {
                    return new string[,]
                    {
                        { "*", "*", ".", ".", "." },
                        { ".", ".", ".", ".", "." },
                        { ".", "*", ".", ".", "." }
                    };
                }

            case 2:
                {
                    return new string[,]
                    {
                        { "*", ".", ".", "*", ".", ".", ".", ".", ".", ".", "." },
                        { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
                        { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "*" },
                        { ".", ".", ".", ".", "*", "*", "*", ".", ".", ".", "." },
                        { ".", ".", ".", ".", "*", ".", "*", ".", ".", "*", "." },
                        { ".", ".", ".", ".", "*", "*", "*", ".", ".", "*", "." },
                        { ".", ".", "*", ".", ".", ".", ".", ".", ".", ".", "." },
                        { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." },
                        { ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "." }
                    };
                }
            default: throw new ArgumentException("Board index not found!");
        }
    }
}


