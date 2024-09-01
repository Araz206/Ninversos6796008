using SQLite;

namespace Ninversos6796008
{
    public class AppDatabase
    {
        private const string DB_NAME = "inverso.db3";
        private readonly SQLiteAsyncConnection _connection;

        public AppDatabase()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Numero>();
        }

        public async Task<List<Numero>> GetNumero()
        {
            return await _connection.Table<Numero>().ToListAsync();
        }

        public async Task<Numero> GetById(int id)
        {
            return await _connection.Table<Numero>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Numero numero)
        {
            await _connection.InsertAsync(numero);
        }

        public async Task Update(Numero numero
            )
        {
            await _connection.UpdateAsync(numero);
        }

        public async Task Delete(Numero numero)
        {
            await _connection.DeleteAsync(numero);
        }
    }
}
