using System;

namespace Projet7
{
    public class GameEndedEventArgs : EventArgs
    {
        public int Joueur { get; private set; }
        public Jeu Jeu { get; private set; }

        public GameEndedEventArgs(int Joueur, Jeu jeu)
        {
            this.Joueur = Joueur;
            this.Jeu = new Jeu(jeu);
        }
    }
}