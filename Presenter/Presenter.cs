using MutableDataGridSync.Factories;
using MutableDataGridSync.Models;
using MutableDataGridSync.Repositories;
using MutableDataGridSync.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutableDataGridSync.Presenter
{
    class Presenter
    {
        private const string idColumn = nameof(Lichtpunkt.ogc_fid);
        private readonly View _view;
        private DataTable dataTable;
        private LichtpunktRepository _lichtpunktRepository;

        public Presenter(View view)
        {
            _view = view;
            _lichtpunktRepository = SqliteFactory.GetLichtpunktRepository();
            _lichtpunktRepository.Create();
            dataTable = _lichtpunktRepository.GetDataTable();

            _view.DataSource = dataTable;

            var lps = Enumerable.Range(0, 2).Select(i => new Lichtpunkt { Ort = "Ort" + i, Straße = "Straße" + i });
            foreach (var lp in lps)
            {
                _lichtpunktRepository.Add(lp);
            }
            _lichtpunktRepository.Update();

            InitEventListeners();
        }

        private void InitEventListeners()
        {
            _view.UpdateTable += (sender, args) =>
            {
                _lichtpunktRepository.Update();
                Console.WriteLine("Updated");
            };
            _view.RowChanged += (sender, row) =>
            {
                row.SetField(idColumn, Guid.NewGuid().ToString());
            };
        }
    }
}
