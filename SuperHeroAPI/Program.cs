var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddTransient<ISuperHeroService, SuperHeroService>();
builder.Services.AddTransient<ISuperHeroRepository, SuperHeroRepository>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<ITeamRepository, TeamRepository>();

builder.Services.AddDbContext<SuperHeroDbContext>( options => {
    options.UseSqlServer( builder.Configuration.GetConnectionString( "Default" ) );
} );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors( policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );

app.UseAuthorization();

app.MapControllers();

app.Run();
