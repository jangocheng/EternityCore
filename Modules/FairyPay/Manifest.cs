using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "FairyPay",
    Author = "Eternity",
    Version = "1.0.0",
    Description = "可爱支付，多商户多渠道支付平台",
    Dependencies = new[] { "Settings", "OrchardCore.Data.EntityFrameworkCore" },
    Category = "我的模块"
    )]