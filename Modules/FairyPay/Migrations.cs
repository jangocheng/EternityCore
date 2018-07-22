using System;
using System.Data;
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
                t.Column(nameof(PayType.Id), DbType.AnsiString, c => { c.WithLength(20); });
                t.Column(nameof(PayType.Name), DbType.String, c => { c.WithLength(50); });
            });

            SchemaBuilder.CreateTable
                (nameof(PayProviderSettings), t =>
                {
                    t.Column<int>(nameof(PayProviderSettings.Id),  c => c.Identity().PrimaryKey());
                    t.Column<string>(nameof(PayProviderSettings.ProviderId), c => c.WithLength(20));
                    t.Column<string>(nameof(PayProviderSettings.Settings), c => c.WithLength(1000));
                });
            return 1;
        }

    }
}
