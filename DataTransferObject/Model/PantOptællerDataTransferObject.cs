using DataTransferObject.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Model
{
    public sealed class PantOptæller
    {
        private static PantOptæller _instance;
        private static readonly object _lock = new object();

        private int _løseØlTæller = 0;
        private const int FlaskerPrKasse = 30;

        public Dictionary<PantDataTransferObject, int> OpsamletPant { get; private set; }

        private readonly Dictionary<PantDataTransferObject, decimal> _pantVærdier = new()
        {
            { PantDataTransferObject.A, 1.00m },
            { PantDataTransferObject.B, 1.50m },
            { PantDataTransferObject.C, 3.00m },
            { PantDataTransferObject.Kasse, 12.50m },
            { PantDataTransferObject.None, 0.00m }
        };

        private PantOptæller()
        {
            InitialiserRegnskab();
        }
        public static PantOptæller Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new PantOptæller();
                    }
                }
                return _instance;
            }
        }

        private void InitialiserRegnskab()
        {
            OpsamletPant = new Dictionary<PantDataTransferObject, int>();
            foreach (PantDataTransferObject type in Enum.GetValues(typeof(PantDataTransferObject)))
            {
                OpsamletPant[type] = 0;
            }
            _løseØlTæller = 0;
        }

        
        public void TilføjPant(PantDataTransferObject type, int antal)
        {
            if (type == PantDataTransferObject.None || antal <= 0) return;

            OpsamletPant[type] += antal;
        }

        
        public void TilføjØlMedKasseLogik(PantDataTransferObject type, int antal)
        {
            if (antal <= 0) return;

            TilføjPant(type, antal);

            _løseØlTæller += antal;

            while (_løseØlTæller >= FlaskerPrKasse)
            {
                OpsamletPant[PantDataTransferObject.Kasse]++;
                _løseØlTæller -= FlaskerPrKasse;
            }
        }
        public decimal HentTotalPantVærdi()
        {
            decimal total = 0;
            foreach (var post in OpsamletPant)
            {
                if (_pantVærdier.TryGetValue(post.Key, out decimal værdi))
                {
                    total += post.Value * værdi;
                }
            }
            return total;
        }

        
        public int ManglerTilNæsteKasse()
        {
            if (_løseØlTæller == 0) return FlaskerPrKasse;
            return FlaskerPrKasse - _løseØlTæller;
        }

        public void Nulstil()
        {
            lock (_lock)
            {
                InitialiserRegnskab();
            }
        }
    }
}