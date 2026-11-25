namespace CoreGame;

public class Plateau
{
    private int NB_COLUMNS;
    private int NB_ROWS;
    private int[,] plateau;
    
    public Plateau(int columns, int rows)
    {
        NB_ROWS = rows;
        NB_COLUMNS = columns;
        plateau = new int[NB_COLUMNS,NB_ROWS];
        for (int i = 0; i < NB_COLUMNS; i++)
        {
            for (int j = 0; j < NB_ROWS; j++)
            {
                plateau[i, j] = 2;
            }
        }
    }

    public int[,] getPlateau()
    {
        return plateau;
    }
    public int getNbRows()
    {
        return NB_ROWS;
    }

    public int getNbColumns()
    {
        return NB_COLUMNS;
    }
    
    public int getPion(int column, int row)
    {
        return plateau[column,row];
    }

    public int setPion(int id, int column, int row)
    {
        return plateau[column,row] = id;
    }
    
    

}