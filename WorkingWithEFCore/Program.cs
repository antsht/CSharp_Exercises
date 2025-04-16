using Northwind.EntityModels;

using NorthwindDb db = new();
WriteLine($"Provider: {db.Database.ProviderName}");


