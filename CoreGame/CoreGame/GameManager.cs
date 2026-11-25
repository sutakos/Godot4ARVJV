namespace CoreGame;

public class GameManager
{
    private int nbTour;
    private Plateau plateau;

    public GameManager()
    {
        nbTour = 0;
        plateau = new Plateau(7,6); // peut etre dire la taille du tableau pour faire des parties perso
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
        if (plateau.getPion(x, 0) != 2 || x>= plateau.getNbColumns() || x<0)
        {
            // Colonne pleine || x trop élevé || x inférieur a 0
            return false;
        }
        for(int i = plateau.getNbRows()-1;i>=0; i--)
        {
            int id_pion = plateau.getPion(x, i);
            if (id_pion == 2)
            {
                // Si l'endroit est vide
                plateau.setPion(id_player, x,i);
                return true;
            }
        }
        return false;
    }

    public async Task tour(Func<Task<int>> demanderInput) // async pour pouvoir faire rejouer le joueur si coup invalide
    {
        int id_player = 0;
        while (!isWin(id_player))
        {
            nbTour++;
            id_player = calculeID();
            Console.WriteLine($"Le joueur est : {id_player} il y a {nbTour} tour(s)");

            bool coupValide = false;
            while (!coupValide)
            {
                int input = await demanderInput();  // demander une input console/godot
                coupValide = play(id_player,input);
            }
            if (isWin(id_player))
                Console.WriteLine($"Le gagnant est : {id_player} en {nbTour} tour(s)");
        }
    }
    
}