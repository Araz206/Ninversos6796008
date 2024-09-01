using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninversos6796008
{
    [Table("numeros")]
    public class Numero
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("numero_original")]
        public int NumeroOriginal { get; set; }

        [Column("numero_invertido")]
        public int NumeroInvertido { get; set; }
    }
}
