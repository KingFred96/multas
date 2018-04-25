namespace Multas_tA.Migrations
{
    using Multas_tA.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class SequenciaIdAgentes : DbMigration
    {
        // Adicionar uma sequ�ncia para obter IDs de Agentes de forma
        // at�mica.
        public override void Up()
        {
            int maxIdAgente = 0;

            // Obter o �ltimo ID dos agentes, caso j� existam agentes na BD...
            using (var db = new MultasDb())
            {
                maxIdAgente = db.Agentes.Max(x => x.ID) + 1;
            }

            // Sequ�ncias s�o uma forma at�mica de obter n�meros a partir de uma BD.
            // https://docs.microsoft.com/en-us/sql/t-sql/statements/create-sequence-transact-sql?view=sql-server-2017

            // ATEN��O: S� estou a fazer concatena��o porque T-SQL n�o suporta parameters
            // com comandos DDL! NUNCA se deve fazer concatena��o de strings com vari�veis
            // quando se quer fazer uma query SQL, especialmente se os valores s�o user-provided!!
            Sql(@"Create Sequence [dbo].[SeqIdAgente] As Int Start With " + maxIdAgente + ";");
        }
        
        public override void Down()
        {
            Sql("Drop Sequence [dbo].[SeqIdAgente]");
        }
    }
}
