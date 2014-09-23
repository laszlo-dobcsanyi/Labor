﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;

namespace Labor
{
    public struct MinMaxPair<T>
    {
        public T min;
        public T max;

        public MinMaxPair(T _min, T _max)
        {
            min = _min;
            max = _max;
        }
    }

    //

    public class DataToken<T>
    {
        public enum TokenType
        {
            FOUND,
            NOT_FOUND,
            NEW
        }

        public T data;
        public TokenType type;

        public DataToken(T _data) { data = _data; type = TokenType.NEW; }
        public DataToken(T _data, TokenType _type) { data = _data; type = _type; }
    }

    public abstract class Tokenized_Control<T> : Control
    {
        protected DataTable data;
        protected DataGridView table;
        protected List<DataToken<T>> tokens = new List<DataToken<T>>();

        public void InitializeTokens()
        {
            List<T> data = CurrentData();
            foreach (T item in data)
            {
                DataToken<T> token = new DataToken<T>(item);
                AddToken(token);
                tokens.Add(token);
            }
        }

        #region Abstract functions
        protected abstract List<T> CurrentData();

        protected abstract void AddToken(DataToken<T> _token);

        protected abstract void RemoveToken(DataToken<T> _token);
        #endregion

        public override void Refresh()
        {
            // Összes adat lekérdezése
            List<T> data = CurrentData();
            // Minden token beállítása a kereséshez
            foreach (DataToken<T> token in tokens) { token.type = DataToken<T>.TokenType.NOT_FOUND; }

            // A már táblán fennlévő tokenek összevetése a lekért adatokkal
            foreach (T item in data)
            {
                bool found = false;
                foreach (DataToken<T> token in tokens)
                {
                    if (item.Equals(token.data))
                    {
                        // A megtalált token kivétele a keresésből
                        token.type = DataToken<T>.TokenType.FOUND;
                        found = true;
                        break;
                    }
                }

                // Még tokenek között nem szereplő adat hozzáadása
                if (!found) tokens.Add(new DataToken<T>(item));
            }

            // A tábla kiegésszítése a tokenekből származó adatokkal
            List<DataToken<T>> deletable = new List<DataToken<T>>();
            foreach (DataToken<T> token in tokens)
            {
                switch (token.type)
                {
                    case DataToken<T>.TokenType.NEW:
                        AddToken(token);
                        break;

                    case DataToken<T>.TokenType.NOT_FOUND:
                        RemoveToken(token);
                        deletable.Add(token);
                        break;
                }
            }

            // Nem talált tokenek kivétele
            foreach (DataToken<T> token in deletable) { tokens.Remove(token); } 
            
            base.Refresh();
        }
    }

    public abstract class Tokenized_Form<T> : Form
    {
        protected DataTable data;
        protected DataGridView table;
        protected List<DataToken<T>> tokens = new List<DataToken<T>>();

        public void InitializeTokens()
        {
            List<T> data = CurrentData();
            foreach (T item in data)
            {
                DataToken<T> token = new DataToken<T>(item);
                AddToken(token);
                tokens.Add(token);
            }       
        }

        #region Abstract functions
        protected abstract List<T> CurrentData();

        protected abstract void AddToken(DataToken<T> _token);

        protected abstract void RemoveToken(DataToken<T> _token);
        #endregion

        public override void Refresh()
        {
            // Összes adat lekérdezése
            List<T> data = CurrentData();
            // Minden token beállítása a kereséshez
            foreach (DataToken<T> token in tokens) { token.type = DataToken<T>.TokenType.NOT_FOUND; }

            // A már táblán fennlévő tokenek összevetése a lekért adatokkal
            foreach (T item in data)
            {
                bool found = false;
                foreach (DataToken<T> token in tokens)
                {
                    if (item.Equals(token.data))
                    {
                        // A megtalált token kivétele a keresésből
                        token.type = DataToken<T>.TokenType.FOUND;
                        found = true;
                        break;
                    }
                }

                // Még tokenek között nem szereplő adat hozzáadása
                if (!found) tokens.Add(new DataToken<T>(item));
            }

            // A tábla kiegésszítése a tokenekből származó adatokkal
            List<DataToken<T>> deletable = new List<DataToken<T>>();
            foreach (DataToken<T> token in tokens)
            {
                switch (token.type)
                {
                    case DataToken<T>.TokenType.NEW:
                        AddToken(token);
                        break;

                    case DataToken<T>.TokenType.NOT_FOUND:
                        RemoveToken(token);
                        deletable.Add(token);
                        break;
                }
            }

            // Nem talált tokenek kivétele
            foreach (DataToken<T> token in deletable) { tokens.Remove(token); }

            base.Refresh();

            if (Owner != null) Owner.Refresh();
        }
    }
}
