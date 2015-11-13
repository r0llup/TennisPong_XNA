using System;

namespace Projet7
{
    public class SetEndedEventArgs : EventArgs
    {
        public int Joueur { get; private set; }
        public Set Set { get; private set; }

        public SetEndedEventArgs(int joueur, Set set)
        {
            this.Joueur = joueur;
            this.Set = new Set(set);
        }
    }
}