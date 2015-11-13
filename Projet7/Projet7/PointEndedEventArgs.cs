using System;

namespace Projet7
{
    public class PointEndedEventArgs : EventArgs
    {
        public int Joueur { get; private set; }

        public PointEndedEventArgs(int joueur)
        {
            this.Joueur = joueur;
        }
    }
}