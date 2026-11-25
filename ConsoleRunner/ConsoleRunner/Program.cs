using CoreGame;

Console.WriteLine("Puissance 4 - Joueur contre joueur");
Console.WriteLine("Joueur 1 : X | Joueur 2 : O");

var game = new GameManager();
var plateau = game.getPlateau();
AfficherPlateau(plateau);

var joueurActuel = 0;

while (true)
{
    Console.Write($"Joueur {joueurActuel + 1}, choisissez une colonne (1-7) : ");
    var saisie = Console.ReadLine();

    if (saisie is null)
    {
        Console.WriteLine("Fin de la saisie.");
        break;
    }

    if (!int.TryParse(saisie, out var colonne) || colonne < 1 || colonne > 7)
    {
        Console.WriteLine("Entrée invalide. Choisissez un nombre entre 1 et 7.");
        continue;
    }

    if (!game.getCoupValide().Contains(colonne - 1))
    {
        Console.WriteLine("Cette colonne est pleine. Choisissez-en une autre.");
        continue;
    }

    var (victoire, etatPlateau) = game.tour(colonne);
    var nouveauPlateau = ConvertiPlateau(etatPlateau, game.getCOLUMNS(), game.getROWS());

    AfficherPlateau(nouveauPlateau);

    if (victoire)
    {
        Console.WriteLine($"Le joueur {joueurActuel + 1} a gagné !");
        break;
    }

    if (game.getCoupValide().Count == 0)
    {
        Console.WriteLine("Match nul : le plateau est plein.");
        break;
    }

    joueurActuel = 1 - joueurActuel;
}

static int[,] ConvertiPlateau(List<float> plateau, int colonnes, int lignes)
{
    if (plateau.Count != colonnes * lignes)
    {
        throw new ArgumentException("La taille de l'etat ne correspond pas aux dimensions du plateau.", nameof(plateau));
    }

    var resultat = new int[colonnes, lignes];
    for (var index = 0; index < plateau.Count; index++)
    {
        var colonne = index / lignes;
        var ligne = index % lignes;
        resultat[colonne, ligne] = (int)plateau[index];
    }

    return resultat;
}
static void AfficherPlateau(int[,] plateau)
{
    Console.WriteLine();
    Console.WriteLine("  1 2 3 4 5 6 7");
    for (var ligne = plateau.GetLength(1) - 1; ligne >= 0; ligne--)
    {
        Console.Write("| ");
        for (var colonne = 0; colonne < plateau.GetLength(0); colonne++)
        {
            var pion = plateau[colonne, ligne];
            var symbole = pion switch
            {
                0 => 'X',
                1 => 'O',
                _ => '.'
            };
            Console.Write($"{symbole} ");
        }

        Console.WriteLine("|");
    }

    Console.WriteLine();
}
