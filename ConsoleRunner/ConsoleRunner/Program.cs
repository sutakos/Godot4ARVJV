using System;
using System.Threading.Tasks;
using CoreGame;

var game = new GameManager();

await game.tour(()=>
{
    int col = int.TryParse(Console.ReadLine(), out int val) ? val : -1;
    return Task.FromResult(col);
});