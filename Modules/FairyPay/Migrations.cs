using System;
using FairyPay.Models;
using OrchardCore.Data.Migration;


namespace FairyPay
{
    public class Migrations : DataMigration
    {

        public int Create()
        {
            SchemaBuilder.CreateTable(nameof(PayType), t =>
            {
                t.Column(nameof(PayType.Id), System.Data.DbType.AnsiString, c => { c.WithLength(20); });
                t.Column(nameof(PayType.Name), System.Data.DbType.String, c => { c.WithLength(50); });
            });
            return 1;
        }

    }
}
