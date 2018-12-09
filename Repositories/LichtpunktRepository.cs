using MutableDataGridSync.Data;
using MutableDataGridSync.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutableDataGridSync.Repositories
{
    class LichtpunktRepository
    {
        private const string Table = nameof(Lichtpunkt);
        private readonly DbContext _context;
        private DataTable _table;

        public LichtpunktRepository(DbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var query = $"CREATE TABLE IF NOT EXISTS {Table} " +
                $"(ogc_fid TEXT DEFAULT (hex(randomblob(16))) PRIMARY KEY, ort TEXT, Straße TEXT)";

            _context.Execute(query);
        }

        public DataTable GetDataTable()
        {
            var query = "SELECT ogc_fid, ort, straße from " + Table;

            using (var conneciton = _context.GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            using (var adapter = new SQLiteDataAdapter(command))
            {
                if (_table == null)
                {
                    _table = new DataTable();
                }
                adapter.Fill(_table);

                return _table;
            }
        }

        public void Update()
        {
            var query = "SELECT ogc_fid, ort, straße from " + Table;

            using (var conneciton = _context.GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            using (var adapter = new SQLiteDataAdapter(command))
            {
                var builder = new SQLiteCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.Update(_table);
            }
        }

        public void Add(Lichtpunkt lichtpunkt)
        {
            var query = $"INSERT INTO {Table}(ort, straße) VALUES (@ort, @straße)";

            using (var conneciton = _context.GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            {
                command.Parameters.AddWithValue("@ort", lichtpunkt.Ort);
                command.Parameters.AddWithValue("@straße", lichtpunkt.Straße);

                command.ExecuteNonQuery();
            }
        }
    }
}
