using AlcaldiaApi.Business.Interfaces;
using AlcaldiaApi.Business.Services.Login;
using AlcaldiaApi.Business.Services.Pruebas;
using AlcaldiaApi.Business.Services.Usuario;
using AlcaldiaApi.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*CONEXION BD*/
builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddCors(options =>
    {
        //var frontendURL = builder.Configuration.GetValue<string>("frontEndURL");
        //options.AddDefaultPolicy(b =>
        //{
        //    b.WithOrigins(frontendURL).AllowAnyMethod();
        //});

        options.AddPolicy("AllowAnything", options =>
        {
            options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jtw"])),
        ClockSkew = TimeSpan.Zero
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
});

builder.Services.AddTransient<ILoginServices, LoginServices>();
builder.Services.AddTransient<IUsuarioServices, UsuarioServices>();

/*Inyeccion de Indenpendencia con 'Key' */
builder.Services.AddKeyedTransient<IUsuarioServices, UsuariosServicesV2>("usuariosServicesV2");

/*EJEMPLO DE TIPOS DE INYECCION DE INDEPENDIENCIAS*/
builder.Services.AddKeyedSingleton<IPruebasServices, PruebasServices>("pruebasSingleton"); //SIEMPRE ES EL MISMO OBJETO PARA OBJETO PARA CADA CLIENTE             
builder.Services.AddKeyedScoped<IPruebasServices, PruebasServices>("pruebasScoped"); //EL OBJETO SERA DISTINTO EN CADA SOLICITUD POR CLIENTE, EJEMPLO: CLIENTE1 (A1,A1,A1) - CLIENTE2 (A2,A2,A2,)
builder.Services.AddKeyedTransient<IPruebasServices, PruebasServices>("pruebasTransient"); //EL OBJETO ES DIFERENTE POR CADA SOLICITUD 

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

app.UseAuthorization();

app.MapControllers();

app.Run();
