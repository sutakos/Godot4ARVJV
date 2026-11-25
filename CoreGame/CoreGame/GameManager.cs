using System.Collections;
using System.Runtime.InteropServices;

namespace CoreGame;

public class GameManager
{
    private int nbTour;
    private Plateau plateau;
    private int COLUMNS = 7;
    private int ROWS = 6;
    private List<int> coupValide;

    public GameManager()
    {
        nbTour = 0;
        plateau = new Plateau(COLUMNS,ROWS); // peut etre dire la taille du tableau pour faire des parties perso
        coupValide = new List<int>();
        for (int i = 0; i < COLUMNS; i++)
        {
            coupValide.Add(i);
        }

    }
    // Le joueur 1 a l'id 1 et le joueur 2 a l'id 0
    private int calculeID()
    {
        return nbTour%2;
    }

    private bool isWin(int id_player)
    {
        for (int i = 0; i < plateau.getNbColumns(); i++)
        {
            for (int j = 0; j < plateau.getNbRows(); j++)
            {
                if (plateau.getPion(i, j) != id_player)
                    continue;

                // Teste les 4 alignements possibles
                if (CheckDirection(i, j, 0, 1, id_player) ||  // Droite
                    CheckDirection(i, j, 1, 0, id_player) ||  // Bas
                    CheckDirection(i, j, 1, 1, id_player) ||  // Diagonale bas-droite
                    CheckDirection(i, j, 1, -1, id_player))   // Diagonale bas-gauche
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckDirection(int col, int row, int dRow, int dCol, int id_player)
    {
        // Verification qu'on ne sort pas du plateau
        int endRow = row + 3 * dRow;
        int endCol = col + 3 * dCol;
        if (endRow < 0 || endRow >= plateau.getNbRows() || endCol < 0 || endCol >= plateau.getNbColumns())
            return false;

        // Verifie les 3 cases suivantes dans cette direction (1ere deja valide)
        for (int k = 1; k < 4; k++)
        {
            if (plateau.getPion(col + k * dCol, row + k * dRow) != id_player)
                return false;
        }
        return true;
    }

    private bool play(int id_player,int x)
    {
        if (coupValide.Contains(x))
        {
            for (int i = plateau.getNbRows() - 1; i >= 0; i--)
            {
                int id_pion = plateau.getPion(x, i);
                if (id_pion == 2)
                {
                    // Si l'endroit est vide
                    plateau.setPion(id_player, x, i);
                    return true;
                }
            }
        }
        return false;
    }

    public List<int> getCoupValide()
    {
        return coupValide;
    }

    public List<float> getState()
    {
        int[,] tmp = plateau.getPlateau();
        List<float> state = new List<float>();
        foreach (int i in tmp)
        {
            state.Add(i);
        }
        return state;
    }

    public int[,] getPlateau()
    {
        return plateau.getPlateau();
    }
    
    public (bool,List<float>) tour(int coupJouer)
    {
        
        int idPlayer = calculeID();
        int colonne = coupJouer;
        colonne--;
        play(idPlayer, colonne);
        nbTour++;
        envoyerCoupPossible(ref coupValide);
        return (isWin(idPlayer),getState());
    }

    public int getROWS()
    {
        return ROWS;
    }

    public int getCOLUMNS()
    {
        return COLUMNS;
    }

    public void envoyerCoupPossible(ref List<int> coupPosssible)
    {
        coupPosssible.RemoveAll(i =>
            i < 0 ||
            i >= plateau.getNbColumns() ||
            plateau.getPion(i, 0) != 2);
    }
}