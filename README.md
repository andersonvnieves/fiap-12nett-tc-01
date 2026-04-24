# fiap-12nett-tc-01
Tech Challenge - phase 1 


dotnet ef migrations add InitialCreate --project br.com.fiap.cloudgames.Infrastructure --startup-project br.com.fiap.cloudgames.WebAPI --output-dir Persistence/Migrations


dotnet ef migrations add Identity --project br.com.fiap.cloudgames.Infrastructure --startup-project br.com.fiap.cloudgames.WebAPI --output-dir Persistence/Migrations


dotnet ef database update --project br.com.fiap.cloudgames.Infrastructure --startup-project br.com.fiap.cloudgames.WebAPI 
